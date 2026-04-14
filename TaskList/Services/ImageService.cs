using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Test
{
    /// <summary>
    /// File-system operations for task-attached images: copy, paste, remove, and directory layout.
    /// No WinForms dependency — safe to reuse in any host application.
    /// (System.Drawing is used for clipboard-image saving but carries no WinForms coupling.)
    /// </summary>
    public class ImageService
    {
        private readonly string      _imagesDir;
        private readonly TaskService _taskService;

        public static readonly string[] Extensions =
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".tif", ".webp"
        };

        public ImageService(string baseDir, TaskService taskService)
        {
            _imagesDir   = Path.Combine(baseDir, "task_images");
            _taskService = taskService;
        }

        public string GetTaskImageDir(TaskItem task) => Path.Combine(_imagesDir, task.Id);

        /// <summary>
        /// Copies image files into the task's image directory, deduplicating filenames.
        /// Only files with supported extensions are processed.
        /// Returns true if at least one image was added.
        /// </summary>
        public bool CopyImages(TaskItem task, string[] sourcePaths)
        {
            if (sourcePaths.Length == 0) return false;
            string taskDir = GetTaskImageDir(task);
            Directory.CreateDirectory(taskDir);
            bool any = false;
            foreach (string src in sourcePaths)
            {
                if (!Extensions.Contains(Path.GetExtension(src).ToLowerInvariant())) continue;
                string name = UniqueFileName(taskDir, Path.GetFileName(src));
                try { File.Copy(src, Path.Combine(taskDir, name)); }
                catch { continue; }
                task.ImagePaths.Add(name);
                any = true;
            }
            if (any) _taskService.SaveAll();
            return any;
        }

        /// <summary>
        /// Saves a clipboard image as a PNG in the task's image directory.
        /// The caller retains ownership of <paramref name="image"/> and must dispose it.
        /// Returns true on success.
        /// </summary>
        public bool SaveClipboardImage(TaskItem task, Image image)
        {
            string taskDir  = GetTaskImageDir(task);
            Directory.CreateDirectory(taskDir);
            string filename = $"paste_{DateTime.Now:yyyyMMdd_HHmmss_fff}.png";
            string dest     = Path.Combine(taskDir, filename);
            try { image.Save(dest, ImageFormat.Png); }
            catch { return false; }
            task.ImagePaths.Add(filename);
            _taskService.SaveAll();
            return true;
        }

        /// <summary>Removes an image from the task and deletes the file.</summary>
        public void RemoveImage(TaskItem task, string filename)
        {
            string path = Path.Combine(GetTaskImageDir(task), filename);
            task.ImagePaths.Remove(filename);
            try { if (File.Exists(path)) File.Delete(path); } catch { }
            _taskService.SaveAll();
        }

        private static string UniqueFileName(string dir, string name)
        {
            if (!File.Exists(Path.Combine(dir, name))) return name;
            string stem = Path.GetFileNameWithoutExtension(name);
            string ext  = Path.GetExtension(name);
            int n = 1;
            string candidate;
            do { candidate = $"{stem}_{n++}{ext}"; } while (File.Exists(Path.Combine(dir, candidate)));
            return candidate;
        }
    }
}

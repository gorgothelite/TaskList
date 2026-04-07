from PIL import Image

img = Image.open("ai-integration.png")  # your 512x512 PNG
img.save("ai-integration.ico", sizes=[(16,16),(32,32),(48,48),(256,256)])

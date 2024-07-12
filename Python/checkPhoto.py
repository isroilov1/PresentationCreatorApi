from PIL import Image

async def open_image(image_path):
    # Pillow kutubxonasida qo'llab-quvvatlanadigan rasm formatlari
    SUPPORTED_FORMATS = {'BMP', 'GIF', 'JPEG', 'PNG', 'JPG', 'TIFF', 'WMF'}
    try:
        # Rasm formatini aniqlash
        with Image.open(image_path) as img:
            format = img.format
            print(format)
            if format not in SUPPORTED_FORMATS:
                print("Not supported")
                return False
            # Agar rasm formati qo'llab-quvvatlansa, rasmdan foydalanish
            print("Supported")
            return True
    except Exception as e:
        # Umumiy xatolarni ushlash
        print(f"An error occurred: {e}")
    return False

from PIL import Image
import math

def modify_pixel(r, g, b, a):
    new_r = math.ceil(0.8 * r + 17)
    new_g = math.ceil(0.8 * g + 34.01)
    new_b = math.ceil(0.8 * b + 37.01)
    return new_r, new_g, new_b, a

# Open an image file
input_image_path = "input.png"
output_image_path = "output.png"
image = Image.open(input_image_path).convert("RGBA")

# Get the dimensions of the image
width, height = image.size

# Loop through every pixel in the image
for x in range(width):
    for y in range(height):
        # Get the RGB values of the pixel
        r, g, b, a = image.getpixel((x, y))

        # Modify the RGB values using the custom function
        new_r, new_g, new_b, new_a = modify_pixel(r, g, b, a)

        # Update the pixel with the new RGB values
        image.putpixel((x, y), (new_r, new_g, new_b, new_a))

# Save the modified image
image.save(output_image_path)

print("Image processing complete. Output saved as", output_image_path)
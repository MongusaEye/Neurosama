import os
from PIL import Image

def add_pixel_gaps(img, gap_width=1):
    # Get the dimensions of the original image
    width, height = img.size
    
    # Calculate the new dimensions with gaps
    new_width = width + (width // 8) * gap_width
    new_height = height + (height // 8) * gap_width
    
    # Create a new image with the increased width and height
    new_img = Image.new('RGBA', (new_width, new_height))
    
    # Paste the original image onto the new image with gaps (both horizontally and vertically)
    x_offset = 0
    y_offset = 0
    for x in range(0, width, 8):
        for y in range(0, height, 8):
            region = img.crop((x, y, x + 8, y + 8))
            new_img.paste(region, (x_offset, y_offset))
            y_offset += 8 + gap_width
        x_offset += 8 + gap_width
        y_offset = 0
    
    return new_img

def upscale_nearest_neighbor(image):
    # Upscale the image 2x using nearest-neighbor interpolation
    upscaled_img = image.resize((image.width * 2, image.height * 2), Image.Resampling.NEAREST)
    return upscaled_img

def process_images(input_folder="."):
    # Iterate through image range
    for i in ["ErmFishCage","ErmFishGoldCage"]:
        # Construct path to input image
        input_image_path = os.path.join(input_folder, f"_{i}.png")
        
        # Check if input image exists
        if os.path.exists(input_image_path):
            # Open input image
            input_image = Image.open(input_image_path)

            # Process the image: downscale and remove pixel gaps
            image_with_gaps = add_pixel_gaps(input_image)
            final_image = upscale_nearest_neighbor(image_with_gaps)

            # Construct path to output image
            output_image_path = os.path.join(input_folder, f"{i}.png")
            
            # Save the corrected image
            final_image.save(output_image_path)
            print(f"Processed image {i}.")

if __name__ == "__main__":
    process_images()
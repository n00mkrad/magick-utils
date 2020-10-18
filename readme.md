# MagickUtils

#### ImageMagick (Magick.NET) based image processing toolkit for image conversion and dataset preparation

![](https://i.imgur.com/1UtnQhO.png)

[**Download latest release**](https://github.com/n00mkrad/magick-utils/releases)

## Functionality

Conversion Tools:

- Convert to JPEG, PNG, WEBP, BMP, DDS, TGA, JPEG 2000, AVIF, FLIF, HEIF
- Set image quality (can be randomized for some formats, for dataset creation)

Scaling Tools:

- Scale (resize) or Resample (scale image, then resize it to the original canvas size)
- Scaling Modes: Percentage, Pixels Width, Pixels Height, Pixels Shorter Side, Pixels Longer Side
- Several scaling filters (Nearest, Bicubic, Mitchell, ...) are supported
- Option to randomize filtering and/or append filter name to filename

Crop/Expand Tools:

- Crop/expand using percentage
- Crop/expand to exact size
- Crop/expand to a divisible resolution (e.g. make sure image size is divisible by 8 for downscaling)
- Tile images or merge them

File Handling Tools:

- Add prefix/suffix, replace text in filenames, rename using counter, add zero-padding, much more
- Delete images by size using a certain logic (bigger/smaller/exactly/divisible by/not divisible by)
- Remove an amount of bytes from the beginning of each file (for wrapped texture formats like TEX -> DDS)

Color Tools:

* Auto-Level
* Remove transparency and fill with color
* Reduce color depth
* Layer Color On Top
* Dithering, optionally with a random amount of colors in a range

Effects Tools:

- Add Noise or Blur
- Run Median Filter
- Run Edge Detection
- Add sharpening halos

Inpainting Tools:

- Erase parts of images using a specified color

Geometry Tools:

- Rotate or Flip images, both can optionally be randomized






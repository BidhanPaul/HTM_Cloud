// Copyright (c) Damir Dobric. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Daenet.ImageBinarizerLib;
using Daenet.ImageBinarizerLib.Entities;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace NeoCortex
{
    /// <summary>
    /// Set of helper methods.
    /// </summary>
    public class NeoCortexUtils
    {
        /// <summary>
        /// Binarize image to the file with the test name.
        /// </summary>
        /// <param name="mnistImage"></param>
        /// <param name="imageSize"></param>
        /// <param name="testName"></param>
        /// <returns></returns>
        public static string BinarizeImage(string mnistImage, int imageSize, string testName)
        {
            string binaryImage;

            binaryImage = $"{testName}.txt";

            if (File.Exists(binaryImage))
                File.Delete(binaryImage);

            ImageBinarizer imageBinarizer = new ImageBinarizer(new BinarizerParams { RedThreshold = 200, GreenThreshold = 200, BlueThreshold = 200, ImageWidth = imageSize, ImageHeight = imageSize, InputImagePath = mnistImage, OutputImagePath = binaryImage });

            imageBinarizer.Run();

            return binaryImage;
        }

        /// <summary>
        /// Draws the bitmap from array of active columns.
        /// </summary>
        /// <param name="twoDimArray">Array of active columns.</param>
        /// <param name="width">Output width.</param>
        /// <param name="height">Output height.</param>
        /// <param name="filePath">The bitmap PNG filename.</param>
        /// <param name="text">Text to be written.</param>
        public static void DrawBitmap(int[,] twoDimArray, int width, int height, String filePath, string text = null)
        {
            DrawBitmap(twoDimArray, width, height, filePath, Color.Black, Color.Green, text);
        }

        /// <summary>
        /// Draws the bitmap from array of active columns.
        /// </summary>
        /// <param name="twoDimArray">Array of active columns.</param>
        /// <param name="width">Output width.</param>
        /// <param name="height">Output height.</param>
        /// <param name="filePath">The bitmap PNG filename.</param>
        /// <param name="inactiveCellColor"></param>
        /// <param name="activeCellColor"></param>
        /// <param name="text">Text to be written.</param>
        public static void DrawBitmap(int[,] twoDimArray, int width, int height, String filePath, Color inactiveCellColor, Color activeCellColor, string text = null)
        {
            int w = twoDimArray.GetLength(0);
            int h = twoDimArray.GetLength(1);

            if (w > width || h > height)
                throw new ArgumentException("Requested width/height must be greather than width/height inside of array.");

            var scale = width / w;

            if (scale * w < width)
                scale++;

            DrawBitmap(twoDimArray, scale, filePath, inactiveCellColor, activeCellColor, text);

        }

        /// <summary>
        /// Draws the bitmap from array of active columns.
        /// </summary>
        /// <param name="twoDimArray">Array of active columns.</param>
        /// <param name="scale">Scale of bitmap. If array of active columns is 10x10 and scale is 5 then output bitmap will be 50x50.</param>
        /// <param name="filePath">The bitmap filename.</param>
        /// <param name="activeCellColor"></param>
        /// <param name="inactiveCellColor"></param>
        /// <param name="text">Text to be written.</param>
        public static void DrawBitmap(int[,] twoDimArray, int scale, String filePath, Color inactiveCellColor, Color activeCellColor, string text = null)
        {
            int w = twoDimArray.GetLength(0);
            int h = twoDimArray.GetLength(1);

            System.Drawing.Bitmap myBitmap = new System.Drawing.Bitmap(w * scale, h * scale);
            int k = 0;
            for (int Xcount = 0; Xcount < w; Xcount++)
            {
                for (int Ycount = 0; Ycount < h; Ycount++)
                {
                    for (int padX = 0; padX < scale; padX++)
                    {
                        for (int padY = 0; padY < scale; padY++)
                        {
                            if (twoDimArray[Xcount, Ycount] == 1)
                            {
                                //myBitmap.SetPixel(Xcount, Ycount, System.Drawing.Color.Yellow); // HERE IS YOUR LOGIC
                                myBitmap.SetPixel(Xcount * scale + padX, Ycount * scale + padY, activeCellColor); // HERE IS YOUR LOGIC
                                k++;
                            }
                            else
                            {
                                //myBitmap.SetPixel(Xcount, Ycount, System.Drawing.Color.Black); // HERE IS YOUR LOGIC
                                myBitmap.SetPixel(Xcount * scale + padX, Ycount * scale + padY, inactiveCellColor); // HERE IS YOUR LOGIC
                                k++;
                            }
                        }
                    }
                }
            }

            Graphics g = Graphics.FromImage(myBitmap);
            var fontFamily = new FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif);
            g.DrawString(text, new Font(fontFamily, 32), SystemBrushes.Control, new PointF(0, 0));

            myBitmap.Save(filePath, ImageFormat.Png);
        }

        /// <summary>
        /// TODO: add comment
        /// </summary>
        /// <param name="twoDimArrays"></param>
        /// <param name="filePath"></param>
        /// <param name="bmpWidth"></param>
        /// <param name="bmpHeight"></param>
        public static void DrawBitmaps(List<int[,]> twoDimArrays, String filePath, int bmpWidth = 1024, int bmpHeight = 1024)
        {
            DrawBitmaps(twoDimArrays, filePath, Color.DarkGray, Color.Yellow, bmpWidth, bmpHeight);
        }


        /// <summary>
        /// Drawas bitmaps from list of arrays.
        /// </summary>
        /// <param name="twoDimArrays">List of arrays to be represented as bitmaps.</param>
        /// <param name="filePath">Output image path.</param>
        /// <param name="inactiveCellColor">Color of inactive bit.</param>
        /// <param name="activeCellColor">Color of active bit.</param>
        /// <param name="bmpWidth">The width of the bitmap.</param>
        /// <param name="bmpHeight">The height of the bitmap.</param>
        public static void DrawBitmaps(List<int[,]> twoDimArrays, String filePath, Color inactiveCellColor, Color activeCellColor, int bmpWidth = 1024, int bmpHeight = 1024)
        {
            int widthOfAll = 0, heightOfAll = 0;

            foreach (var arr in twoDimArrays)
            {
                widthOfAll += arr.GetLength(0);
                heightOfAll += arr.GetLength(1);
            }

            if (widthOfAll > bmpWidth || heightOfAll > bmpHeight)
                throw new ArgumentException("Size of all included arrays must be less than specified 'bmpWidth' and 'bmpHeight'");

            System.Drawing.Bitmap myBitmap = new System.Drawing.Bitmap(bmpWidth, bmpHeight);
            int k = 0;

            for (int n = 0; n < twoDimArrays.Count; n++)
            {
                var arr = twoDimArrays[n];

                int w = arr.GetLength(0);
                int h = arr.GetLength(1);

                var scale = ((bmpWidth) / twoDimArrays.Count) / (w + 1);// +1 is for offset between pictures in X dim.

                //if (scale * (w + 1) < (bmpWidth))
                //    scale++;

                for (int Xcount = 0; Xcount < w; Xcount++)
                {
                    for (int Ycount = 0; Ycount < h; Ycount++)
                    {
                        for (int padX = 0; padX < scale; padX++)
                        {
                            for (int padY = 0; padY < scale; padY++)
                            {
                                if (arr[Xcount, Ycount] == 1)
                                {
                                    myBitmap.SetPixel(n * (bmpWidth / twoDimArrays.Count) + Xcount * scale + padX, Ycount * scale + padY, activeCellColor); // HERE IS YOUR LOGIC
                                    k++;
                                }
                                else
                                {
                                    myBitmap.SetPixel(n * (bmpWidth / twoDimArrays.Count) + Xcount * scale + padX, Ycount * scale + padY, inactiveCellColor); // HERE IS YOUR LOGIC
                                    k++;
                                }
                            }
                        }
                    }
                }
            }

            myBitmap.Save(filePath, ImageFormat.Png);
        }

        /// <summary>
        /// Combines heatmap and normalized permanence representations into a single image with title.
        /// This Drwaitng Function is used to Visulalization of the Permanence Values.
        /// </summary>
        /// <param name="heatmapData">List of arrays representing the heatmap data.</param>
        /// <param name="normalizedData">List of arrays representing normalized data below the heatmap.</param>
        /// <param name="encodedData">List of arrays of original Encoded data encoded by the scaler encoder.</param>
        /// <param name="filePath">Output image path for saving the combined image.</param>
        /// <param name="bmpWidth">Width of the heatmap bitmap (default is 1024).</param>
        /// <param name="bmpHeight">Height of the heatmap bitmap (default is 1024).</param>
        /// <param name="redStart">Threshold for values above which pixels are red (default is 200).</param>
        /// <param name="yellowMiddle">Threshold for values between which pixels are yellow (default is 127).</param>
        /// <param name="greenStart">Threshold for values below which pixels are green (default is 20).</param>
        /// <param name="enlargementFactor">Factor by which the image is enlarged for better visualization (default is 4).</param>
        public static void Draw1dHeatmap(
    List<double[]> heatmapData,
    List<int[]> normalizedData,
    List<int[]> encodedData,
    string filePath,
    int bmpWidth = 1024,
    int bmpHeight = 1024,
    decimal redStart = 200,
    decimal yellowMiddle = 127,
    decimal greenStart = 20,
    int enlargementFactor = 4)
        {
            int height = heatmapData.Count;
            int maxLength = heatmapData.Max(arr => arr.Length);

            if (maxLength > bmpWidth || height > bmpHeight)
                throw new ArgumentException("Size of all included arrays must be less than specified 'bmpWidth' and 'bmpHeight'");

            // Calculate target width and height based on the enlargement factor
            int targetWidth = bmpWidth * enlargementFactor;
            int targetHeight = bmpHeight * enlargementFactor + 40;

            using (var myBitmap = new SKBitmap(targetWidth, targetHeight))
            using (var canvas = new SKCanvas(myBitmap))
            {
                // Set the background color to LightSkyBlue
                canvas.Clear(SKColors.LightSkyBlue);

                // Draw title
                string title = "HeatMap Image";
                var titlePaint = new SKPaint
                {
                    Color = SKColors.Black,
                    TextSize = 12,
                    IsAntialias = true,
                    Typeface = SKTypeface.FromFamilyName("Arial")
                };
                var titleTextBounds = new SKRect();
                titlePaint.MeasureText(title, ref titleTextBounds);
                float titleX = (targetWidth - titleTextBounds.Width) / 2;
                float titleY = 12; // Adjusted value to avoid overlap with top boundary
                canvas.DrawText(title, titleX, titleY, titlePaint);

                // Calculate scale factors for width and height based on the target dimensions
                var scaleX = (double)targetWidth / bmpWidth;
                var scaleY = (double)(targetHeight - 40) / bmpHeight;

                // Leave a gap between sections
                float labelY = 30;

                // Prepare paints for drawing heatmap and lines
                var heatmapPaint = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill
                };

                var linePaint = new SKPaint
                {
                    Color = SKColors.Black,
                    StrokeWidth = 1,
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke
                };

                // Draw heatmap
                for (int i = 0; i < height; i++)
                {
                    var heatmapArr = heatmapData[i];
                    int w = heatmapArr.Length;

                    // Normalize heatmap values for consistent color mapping
                    double minValue = heatmapData.SelectMany(arr => arr).Min();
                    double maxValue = heatmapData.SelectMany(arr => arr).Max();

                    for (int Xcount = 0; Xcount < w; Xcount++)
                    {
                        // Normalize the heatmap value
                        decimal normalizedValue = (decimal)((heatmapArr[Xcount] - minValue) / (maxValue - minValue));

                        for (int padX = 0; padX < scaleX; padX++)
                        {
                            for (int padY = 0; padY < scaleY; padY++)
                            {
                                var color = GetColorSkia(redStart, yellowMiddle, greenStart, normalizedValue);
                                heatmapPaint.Color = color;
                                canvas.DrawPoint((float)(i * scaleX) + (float)(Xcount * scaleX) + padX, (float)padY + labelY, heatmapPaint);
                            }
                        }
                    }

                    // Draw normalized representation below the heatmap
                    var normalizedPaint = new SKPaint
                    {
                        Color = SKColors.Black,
                        TextSize = 10,
                        IsAntialias = true,
                        Typeface = SKTypeface.FromFamilyName("Arial")
                    };
                    string normalizedLabel = "Normalized Permanence (Reconstructed Inputs)";
                    var normalizedLabelTextBounds = new SKRect();
                    normalizedPaint.MeasureText(normalizedLabel, ref normalizedLabelTextBounds);
                    float normalizedLabelX = (targetWidth - normalizedLabelTextBounds.Width) / 2;
                    labelY += 130;
                    labelY += 70;
                    canvas.DrawText(normalizedLabel, normalizedLabelX, labelY, normalizedPaint);

                    var normalizedArr = normalizedData[i];
                    for (int Xcount = 0; Xcount < normalizedArr.Length; Xcount++)
                    {
                        string formattedNumber = normalizedArr[Xcount].ToString();
                        float textX = (float)(i * scaleX) + (float)(Xcount * scaleX) + (float)(scaleX / 2) - 5;
                        float textY = (float)(bmpHeight * scaleY) + 25;
                        canvas.DrawText(formattedNumber, textX, textY, normalizedPaint);

                        // Draw a line from the top middle of the number to the corresponding heatmap pixel
                        float lineStartX = textX + 5;
                        float lineStartY = textY - 20;
                        float lineEndX = (float)(i * scaleX) + (float)(Xcount * scaleX) + (float)(scaleX / 2);
                        float lineEndY = 300;
                        canvas.DrawLine(lineStartX, lineStartY, lineEndX, lineEndY, linePaint);
                    }

                    // Draw the label for encoded values
                    string encodedLabel = "Encoded Inputs";
                    var encodedPaint = new SKPaint
                    {
                        Color = SKColors.Black,
                        TextSize = 10,
                        IsAntialias = true,
                        Typeface = SKTypeface.FromFamilyName("Arial")
                    };
                    var encodedLabelTextBounds = new SKRect();
                    encodedPaint.MeasureText(encodedLabel, ref encodedLabelTextBounds);
                    float encodedLabelX = (targetWidth - encodedLabelTextBounds.Width) / 2;
                    labelY = 120;
                    labelY -= 50;
                    canvas.DrawText(encodedLabel, encodedLabelX, labelY, encodedPaint);

                    var encodedArr = encodedData[i];
                    for (int Xcount = 0; Xcount < encodedArr.Length; Xcount++)
                    {
                        string formattedNumber = encodedArr[Xcount].ToString();
                        float textX = (float)(i * scaleX) + (float)(Xcount * scaleX) + (float)(scaleX / 2) - 5;
                        float textY = 175;
                        canvas.DrawText(formattedNumber, textX, textY, encodedPaint);

                        // Draw a line from the top middle of the number to the corresponding heatmap pixel
                        float lineStartX = textX + 5;
                        float lineStartY = textY - 20;
                        float lineEndX = (float)(i * scaleX) + (float)(Xcount * scaleX) + (float)(scaleX / 2);
                        float lineEndY = 100;
                        canvas.DrawLine(lineStartX, lineStartY, lineEndX, lineEndY, linePaint);
                    }
                }

                // Save the combined image with heatmap and text row
                using (var image = SKImage.FromBitmap(myBitmap))
                using (var data = image.Encode())
                using (var stream = File.OpenWrite(filePath))
                {
                    data.SaveTo(stream);
                }
            }
        }

        // Helper method to get color based on heatmap value
        private static SKColor GetColorSkia(decimal redStart, decimal yellowMiddle, decimal greenStart, decimal value)
        {
            // Ensure the value is clamped between 0 and 1
            decimal clampedValue = Math.Max(0, Math.Min(1, value));

            // Define color transition points and interpolate colors
            if (clampedValue <= 0.5m)
            {
                // Transition from green to yellow
                decimal proportion = clampedValue * 2; // Map to [0, 1]
                byte red = (byte)(127 + proportion * 128); // Transition from green (0) to yellow (127)
                byte green = (byte)(255); // Yellow has maximum green component
                byte blue = (byte)(0); // No blue component
                return new SKColor(red, green, blue);
            }
            else
            {
                // Transition from yellow to red
                decimal proportion = (clampedValue - 0.5m) * 2; // Map to [0, 1]
                byte red = (byte)(255); // Red color component
                byte green = (byte)(255 - proportion * 255); // Transition from yellow (255) to red (0)
                byte blue = (byte)(0); // No blue component
                return new SKColor(red, green, blue);
            }
        }







        public static void GenerateDifferenceHeatmaps(
    List<int[]> normalizedDataList,
    List<int[]> encodedDataList,
    string outputFolderPath,
    int bmpWidth = 1024,
    int bmpHeight = 1024,
    int enlargementFactor = 4)
        {
            // Ensure both lists have the same length
            if (normalizedDataList.Count != encodedDataList.Count)
                throw new ArgumentException("The lists of normalized data and encoded data must have the same length.");

            // Create the folder if it doesn't exist
            if (!Directory.Exists(outputFolderPath))
            {
                Directory.CreateDirectory(outputFolderPath);
            }

            // Helper method to get color based on difference
            SKColor GetHeatmapColor(int normalizedValue, int encodedValue)
            {
                // Use XOR to determine the color
                int colorValue = (normalizedValue > 0 ? 1 : 0) ^ (encodedValue > 0 ? 1 : 0);
                return colorValue == 1 ? SKColors.Red : SKColors.Green;
            }

            for (int i = 0; i < normalizedDataList.Count; i++)
            {
                var normalizedData = normalizedDataList[i];
                var encodedData = encodedDataList[i];

                // Define the file path with the folder path
                string filePath = Path.Combine(outputFolderPath, $"difference_heatmap_{i + 1}.png");
                Debug.WriteLine($"FilePath: {filePath}");

                if (normalizedData.Length != encodedData.Length)
                    throw new ArgumentException("Normalized data and encoded data arrays must have the same length.");

                // Calculate target width and height based on the enlargement factor
                int targetWidth = bmpWidth * enlargementFactor;
                int targetHeight = bmpHeight * enlargementFactor;

                using (var myBitmap = new SKBitmap(targetWidth, targetHeight))
                using (var canvas = new SKCanvas(myBitmap))
                {
                    // Set the background color to white
                    canvas.Clear(SKColors.White);

                    // Calculate scale factors for width and height
                    var scaleX = (double)targetWidth / bmpWidth;
                    var scaleY = (double)targetHeight / bmpHeight;

                    // Draw heatmap
                    for (int x = 0; x < normalizedData.Length; x++)
                    {
                        // Get the color for this position
                        var color = GetHeatmapColor(normalizedData[x], encodedData[x]);

                        // Apply color to bitmap
                        for (int padX = 0; padX < scaleX; padX++)
                        {
                            for (int padY = 0; padY < scaleY; padY++)
                            {
                                int pixelX = (int)(x * scaleX) + padX;
                                int pixelY = (int)(padY);

                                // Ensure pixel coordinates are within bounds
                                if (pixelX < targetWidth && pixelY < targetHeight)
                                {
                                    myBitmap.SetPixel(pixelX, pixelY, color);
                                }
                            }
                        }
                    }

                    // Save the bitmap
                    using (var image = SKImage.FromBitmap(myBitmap))
                    using (var data = image.Encode())
                    using (var stream = File.OpenWrite(filePath))
                    {
                        data.SaveTo(stream);
                    }
                }

                Debug.WriteLine("Difference heatmap generated and saved successfully.");
                Console.WriteLine("Difference heatmap generated and saved successfully.");
            }
        }

        /// <summary>
        /// Drawas bitmaps from list of arrays.
        /// </summary>
        /// <param name="twoDimArrays">List of arrays to be represented as bitmaps.</param>
        /// <param name="filePath">Output image path.</param>
        /// <param name="bmpWidth">The width of the bitmap.</param>
        /// <param name="bmpHeight">The height of the bitmap.</param>
        /// <param name="greenStart">ALl values below this value are by defaut green.
        /// Values higher than this value transform to yellow.</param>
        /// <param name="yellowMiddle">The middle of the heat. Values lower than this value transforms to green.
        /// Values higher than this value transforms to red.</param>
        /// <param name="redStart">Values higher than this value are by default red. Values lower than this value transform to yellow.</param>
        public static void DrawHeatmaps(List<double[,]> twoDimArrays, String filePath,
            int bmpWidth = 1024,
            int bmpHeight = 1024,
            decimal redStart = 200, decimal yellowMiddle = 127, decimal greenStart = 20)
        {
            int widthOfAll = 0, heightOfAll = 0;

            foreach (var arr in twoDimArrays)
            {
                widthOfAll += arr.GetLength(0);
                heightOfAll += arr.GetLength(1);
            }

            if (widthOfAll > bmpWidth || heightOfAll > bmpHeight)
                throw new ArgumentException("Size of all included arrays must be less than specified 'bmpWidth' and 'bmpHeight'");

            System.Drawing.Bitmap myBitmap = new System.Drawing.Bitmap(bmpWidth, bmpHeight);
            int k = 0;

            for (int n = 0; n < twoDimArrays.Count; n++)
            {
                var arr = twoDimArrays[n];

                int w = arr.GetLength(0);
                int h = arr.GetLength(1);

                var scale = Math.Max(1, ((bmpWidth) / twoDimArrays.Count) / (w + 1));// +1 is for offset between pictures in X dim.

                for (int Xcount = 0; Xcount < w; Xcount++)
                {
                    for (int Ycount = 0; Ycount < h; Ycount++)
                    {
                        for (int padX = 0; padX < scale; padX++)
                        {
                            for (int padY = 0; padY < scale; padY++)
                            {
                                myBitmap.SetPixel(n * (bmpWidth / twoDimArrays.Count) + Xcount * scale + padX, Ycount * scale + padY, GetColor(redStart, yellowMiddle, greenStart, (Decimal)arr[Xcount, Ycount]));
                                k++;
                            }
                        }
                    }
                }
            }

            myBitmap.Save(filePath, ImageFormat.Png);
        }

        /// <summary>
        /// Draws a combined similarity plot based on the given list of similarity values.
        /// This graph can Visulaze the Similarity Bar graph of multiple inputs between the Encoded inputs
        /// and the Reconsturced Inputs using Reconstruct Method.
        /// </summary>
        /// <param name="similarities">The list of similarity values to be plotted.</param>
        /// <param name="filePath">The file path where the plot image will be saved.</param>
        /// <param name="imageWidth">Width of the graph.</param>
        /// <param name="imageHeight">Height of the graph.</param>
        /// <remarks>
        /// The plot includes bars representing similarity values, indexed from left to right. Each bar's height corresponds to its similarity value.
        /// Axis labels, a title, a scale indicating similarity values, and text indicating the similarity range are added to the plot.
        /// </remarks>

        public static void DrawCombinedSimilarityPlot(List<double> similarities, string filePath, int imageWidth, int imageHeight)
        {
            // Create a new SKBitmap
            using (var bitmap = new SKBitmap(imageWidth, imageHeight))
            using (var canvas = new SKCanvas(bitmap))
            {
                // Clear the bitmap with a white background
                canvas.Clear(SKColors.White);

                // Define the maximum similarity value
                double maxSimilarity = similarities.Max();

                // Calculate the maximum bar height based on the plot height and scale
                // Adjusted for title position
                int maxBarHeight = imageHeight - 200;

                // Determine the number of bars
                int barCount = similarities.Count;

                // Calculate the total width occupied by bars and spacing
                // minimum bar width is 10 pixels
                int totalBarWidth = barCount * 10;
                // 20 pixels of spacing between bars
                int totalSpacing = 20 * (barCount + 1);

                // Calculate the maximum available width for bars (excluding margins)
                // Adjusted for margins
                int maxAvailableWidth = imageWidth - totalSpacing - 200;

                // Calculate the bar width based on the available space and number of bars
                // Minimum width for each bar
                int minBarWidth = 20;
                int barWidth = Math.Max(minBarWidth, maxAvailableWidth / barCount);

                // Define the width of the scale
                int scaleWidth = 100;

                // Draw each bar
                for (int i = 0; i < barCount; i++)
                {
                    // Calculate the height of the bar based on the similarity value
                    int barHeight = (int)(similarities[i] / maxSimilarity * maxBarHeight);

                    // Determine the position and size of the bar
                    // Adjusted x position and spacing between bars
                    int x = scaleWidth + (i + 1) * 20 + i * barWidth;
                    // Adjusted for title position and space at the bottom for labels
                    int y = imageHeight - barHeight - 100;

                    // Draw the bar with a minimum width of 1 pixel to avoid disappearance
                    // Subtracting 1 to leave a small gap between bars
                    int w = Math.Max(1, barWidth - 1);

                    // Determine the color based on the similarity level
                    SKColor color = GetColorForSimilarity(similarities[i]);

                    // Create a solid paint with the determined color
                    using (var paint = new SKPaint { Color = color, Style = SKPaintStyle.Fill })
                    {
                        // Draw the bar
                        canvas.DrawRect(x, y, w, barHeight, paint);
                    }

                    // Add labels for each bar
                    // Format the similarity value
                    string label = similarities[i].ToString("0.0");
                    using (var font = new SKPaint { TextSize = 10, IsAntialias = true, Color = SKColors.Black, Typeface = SKTypeface.FromFamilyName("Sans Serif", SKTypefaceStyle.Bold) })
                    {
                        SKRect labelSize = new SKRect();
                        font.MeasureText(label, ref labelSize);
                        // Draw the label above the bar
                        canvas.DrawText(label, x + (barWidth - labelSize.Width) / 2, y - 20, font);
                        // Draw input label below the bar
                        canvas.DrawText($"{i + 1}", x + (barWidth - labelSize.Width) / 2, imageHeight - 50, font);
                    }
                }

                // Add axis labels
                using (var axisFont = new SKPaint { TextSize = 14, IsAntialias = true, Color = SKColors.Black, Typeface = SKTypeface.FromFamilyName("Sans Serif", SKTypefaceStyle.Bold) })
                {
                    canvas.DrawText("X - Axis (Input) Index", scaleWidth + (imageWidth - scaleWidth) / 2, imageHeight - 20, axisFont);
                }

                // Add a title
                string title = "Similarity Graph";
                using (var titleFont = new SKPaint { TextSize = 18, IsAntialias = true, Color = SKColors.Black, Typeface = SKTypeface.FromFamilyName("Sans Serif", SKTypefaceStyle.Bold) })
                {
                    SKRect titleSize = new SKRect();
                    titleFont.MeasureText(title, ref titleSize);
                    // Adjusted title position
                    canvas.DrawText(title, (imageWidth - titleSize.Width) / 2, 20 + titleSize.Height, titleFont);
                }

                // Add a scale indicating values from 0 to 1
                using (var scaleFont = new SKPaint { TextSize = 12, IsAntialias = true, Color = SKColors.Black, Typeface = SKTypeface.FromFamilyName("Sans Serif", SKTypefaceStyle.Bold) })
                {
                    // Draw 11 tick marks
                    for (int i = 0; i <= 10; i++)
                    {
                        double value = i / 10.0;
                        // Invert value and map to plot height
                        int y = (int)((1 - value) * maxBarHeight) + 100;
                        // Draw tick mark
                        canvas.DrawLine(scaleWidth - 10, y, scaleWidth, y, scaleFont);
                        // Draw value label
                        canvas.DrawText(value.ToString("0.0"), 0, y - 8, scaleFont);
                    }
                }

                // Add text indicating the similarity test
                string similarityText = "Y axis-Similarity Range";
                using (var similarityFont = new SKPaint { TextSize = 14, IsAntialias = true, Color = SKColors.Black, Typeface = SKTypeface.FromFamilyName("Sans Serif", SKTypefaceStyle.Bold) })
                {
                    SKRect similaritySize = new SKRect();
                    similarityFont.MeasureText(similarityText, ref similaritySize);
                    canvas.Save();
                    canvas.Translate(50, imageHeight / 2 - similaritySize.Height / 2);
                    canvas.RotateDegrees(-90);
                    canvas.DrawText(similarityText, 0, 0, similarityFont);
                    canvas.Restore();
                }

                // Save the bitmap to a file as PNG format
                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite(filePath))
                {
                    data.SaveTo(stream);
                }
            }
        }

        // Helper method to get color based on similarity level
        private static SKColor GetColorForSimilarity(double similarity)
        {
            // Define a color gradient from green to red
            byte redValue;
            byte greenValue;

            if (similarity > 0.5)
            {
                // Calculate green value based on similarity
                greenValue = (byte)(255 - (similarity - 0.5) * 2 * 255);
                redValue = 255;
            }
            else
            {
                // Calculate red value based on similarity
                redValue = (byte)(similarity * 2 * 255);
                greenValue = 255;
            }

            return new SKColor(redValue, greenValue, 0);
        }


        /// <summary>
        /// Determines the color based on the given similarity level.
        /// </summary>
        /// <param name="similarity">The similarity level to determine the color for (range: 0 to 1).</param>
        /// <returns>The color corresponding to the similarity level, ranging from light gray to dark orange.</returns>



        private static Color GetColor(decimal redStartVal, decimal yellowStartVal, decimal greenStartVal, decimal val)
        {
            // color points
            int[] Red = new int[] { 255, 0, 0 }; //{ 252, 191, 123 }; // #FCBF7B
            int[] Yellow = new int[] { 254, 255, 132 }; // #FEEB84
            int[] Green = new int[] { 99, 190, 123 };  // #63BE7B
            //int[] Green = new int[] { 0, 0, 255 };  // #63BE7B
            int[] White = new int[] { 255, 255, 255 }; // #FFFFFF

            // value that corresponds to the color that represents the tier above the value - determined later
            Decimal highValue = 0.0M;
            // value that corresponds to the color that represents the tier below the value
            Decimal lowValue = 0.0M;
            // next higher and lower color tiers (set to corresponding member variable values)
            int[] highColor = null;
            int[] lowColor = null;

            // 3-integer array of color values (r,g,b) that will ultimately be converted to hex
            int[] rgb = null;


            // If value lower than green start value, it must be green.
            if (val <= greenStartVal)
            {
                rgb = Green;
            }
            // determine if value lower than the baseline of the red tier
            else if (val >= redStartVal)
            {
                rgb = Red;
            }

            // if not, then determine if value is between the red and yellow tiers
            else if (val > yellowStartVal)
            {
                highValue = redStartVal;
                lowValue = yellowStartVal;
                highColor = Red;
                lowColor = Yellow;
            }

            // if not, then determine if value is between the yellow and green tiers
            else if (val > greenStartVal)
            {
                highValue = yellowStartVal;
                lowValue = greenStartVal;
                highColor = Yellow;
                lowColor = Green;
            }
            // must be green
            else
            {
                rgb = Green;
            }

            // get correct color values for values between dark red and green
            if (rgb == null)
            {
                rgb = GetColorValues(highValue, lowValue, highColor, lowColor, val);
            }

            // return the hex string
            return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
        }

        private static int[] GetColorValues(decimal highBound, decimal lowBound, int[] highColor, int[] lowColor, decimal val)
        {

            // proportion the val is between the high and low bounds
            decimal ratio = (val - lowBound) / (highBound - lowBound);
            int[] rgb = new int[3];
            // step through each color and find the value that represents the approriate proportional value 
            // between the high and low colors
            for (int i = 0; i < 3; i++)
            {
                int hc = (int)highColor[i];
                int lc = (int)lowColor[i];
                // high color is lower than low color - reverse the subtracted vals
                bool reverse = hc < lc;

                reverse = false;

                // difference between the high and low values
                int diff = reverse ? lc - hc : hc - lc;
                // lowest value of the two
                int baseVal = reverse ? hc : lc;
                rgb[i] = (int)Math.Round((decimal)diff * ratio) + baseVal;
            }
            return rgb;
        }

        /// <summary>
        /// Determines the color of a bar based on the given similarity level.
        /// </summary>
        /// <param name="similarity">The similarity level to determine the color for.</param>
        /// <returns>The color corresponding to the similarity level.</returns>

        private static Color GetBarColor(double similarity)
        {
            // Assign color based on similarity level
            // High similarity (90% or higher)
            if (similarity >= 0.9)
                return Color.DarkOrange;
            // Medium similarity (70% or higher)
            else if (similarity >= 0.7)
                return Color.Orange;
            // Low similarity (50% or higher)
            else if (similarity >= 0.5)
                return Color.LightSalmon;
            // Very low similarity (below 50%)
            else
                return Color.LightGray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<int> ReadCsvIntegers(String path)
        {
            string fileContent = File.ReadAllText(path);
            string[] integerStrings = fileContent.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> intList = new List<int>();
            for (int n = 0; n < integerStrings.Length; n++)
            {
                String s = integerStrings[n];
                char[] sub = s.ToCharArray();
                for (int j = 0; j < sub.Length; j++)
                {
                    intList.Add(int.Parse(sub[j].ToString()));
                }
            }
            return intList;
        }

        private static Random rnd = new Random(42);

        /// <summary>
        /// Creates the random vector.
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="nonZeroPct"></param>
        /// <returns></returns>
        public static int[] CreateRandomVector(int bits, int nonZeroPct)
        {
            int[] inputVector = new int[bits];

            var nonZeroBits = (float)(nonZeroPct / 100.0) * bits;

            while (inputVector.Count(k => k == 1) < nonZeroBits)
            {
                inputVector[rnd.Next(bits)] = 1;
            }

            return inputVector;
        }

        /// <summary>
        /// Calculate mean value of array of numbers. 
        /// </summary>
        /// <param name="colData"> array of values </param>
        /// <returns>calculated mean</returns>
        public static double MeanOf(double[] colData)
        {
            if (colData == null || colData.Length < 2)
                throw new ArgumentException("'coldData' cannot be null or empty!");

            //calculate summ of the values
            double sum = 0;
            for (int i = 0; i < colData.Length; i++)
                sum += colData[i];

            //calculate mean
            double retVal = sum / colData.Length;

            return retVal;
        }

        /// <summary>
        /// Calculates Pearson correlation coefficient of two data sets
        /// </summary>
        /// <param name="data1"> first data set</param>
        /// <param name="data2">second data set </param>
        /// <returns></returns>
        public static double CorrelationPearson(double[] data1, double[] data2)
        {
            if (data1 == null || data1.Length < 2)
                throw new ArgumentException("'xData' cannot be null or empty!");

            if (data2 == null || data2.Length < 2)
                throw new ArgumentException("'yData' cannot be null or empty!");

            if (data1.Length != data2.Length)
                throw new ArgumentException("Both datasets must be of the same size!");

            //calculate average for each dataset
            double aav = MeanOf(data1);
            double bav = MeanOf(data2);

            double corr = 0;
            double ab = 0, aa = 0, bb = 0;
            for (int i = 0; i < data1.Length; i++)
            {
                var a = data1[i] - aav;
                var b = data2[i] - bav;

                ab += a * b;
                aa += a * a;
                bb += b * b;
            }

            corr = ab / Math.Sqrt(aa * bb);

            return corr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<int> ReadCsvFileTest(String path)
        {
            string fileContent = File.ReadAllText(path);
            string[] integerStrings = fileContent.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> intList = new List<int>();
            for (int n = 0; n < integerStrings.Length; n++)
            {
                String s = integerStrings[n];
                char[] sub = s.ToCharArray();
                for (int j = 0; j < sub.Length; j++)
                {
                    intList.Add(int.Parse(sub[j].ToString()));
                }
            }
            return intList;
        }


        /// <summary>
        /// Creates the 2D box vector.
        /// </summary>
        /// <param name="heightBits">The heght of the vector.</param>
        /// <param name="widthBits">The width of the vector.</param>
        /// <param name="nonzeroBitStart">Position of the first nonzero bit.</param>
        /// <param name="nonZeroBitEnd">Position of the last nonzero bit.</param>
        /// <returns>The two dimensional box.</returns>
        public static int[] Create2DVector(int widthBits, int heightBits, int nonzeroBitStart, int nonZeroBitEnd)
        {
            int[] inputVector = new int[widthBits * heightBits];

            for (int i = 0; i < widthBits; i++)
            {
                for (int j = 0; j < heightBits; j++)
                {
                    if (i > nonzeroBitStart && i < nonZeroBitEnd && j > nonzeroBitStart && j < nonZeroBitEnd)
                        inputVector[i * widthBits + j] = 1;
                    else
                        inputVector[i * 32 + j] = 0;
                }
            }

            return inputVector;
        }

        /// <summary>
        /// Creates the 1D vector.
        /// </summary>
        /// <param name="bits">The number of bits vector.</param>
        /// <param name="nonzeroBitStart">Position of the first nonzero bit.</param>
        /// <param name="nonZeroBitEnd">Position of the last nonzero bit.</param>
        /// <returns>The one dimensional vector.</returns>
        public static int[] CreateVector(int bits, int nonzeroBitStart, int nonZeroBitEnd)
        {
            int[] inputVector = new int[bits];

            for (int j = 0; j < bits; j++)
            {
                if (j > nonzeroBitStart && j < nonZeroBitEnd)
                    inputVector[j] = 1;
                else
                    inputVector[j] = 0;
            }

            return inputVector;
        }


        /// <summary>
        /// Creates the dence array of permancences from sparse array.
        /// </summary>
        /// <param name="array">A dense array of permancences. Ever permanence value is a sum of permanence valus of
        /// active mini-columns to the input neuron with the given index.</param>
        /// <param name="numInputNeurons">Number of input neurons connected from mini-columns at the proximal segment.</param>
        private static double[] CreateDenseArray(Dictionary<int, double> array, int numInputNeurons)
        {
            // Creates the dense array of permanences.
            // Every permanence value for a single input neuron.
            double[] res = new double[numInputNeurons];

            for (int i = 0; i < numInputNeurons; i++)
            {
                if (array.ContainsKey(i))
                    res[i] = array[i];
                else
                    res[i] = 0.0;
            }

            return res;
        }


        /// <summary>
        /// Calculates the softmax function.
        /// </summary>
        /// <param name="sparseArray">The array if indicies of active mini-columns or cells.</param>
        /// <param name="numInputNeurons">The number of existing input neurons.</param>
        /// <returns></returns>
        public static double[] Softmax(Dictionary<int, double> sparseArray, int numInputNeurons)
        {
            var denseArr = CreateDenseArray(sparseArray, numInputNeurons);

            var res = Softmax(denseArr);

            return res;
        }


        /// <summary>
        /// Calculates the softmax of the input array.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The softmax array.</returns>

        public static double[] Softmax(double[] input)
        {
            double[] exponentials = input.Select(x => Math.Exp(x)).ToArray();

            double sum = exponentials.Sum();

            return exponentials.Select(x => x / sum).ToArray();
        }
    }
}

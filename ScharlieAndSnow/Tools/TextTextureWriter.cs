
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScharlieAndSnow
{
    public class TextWriterDropShadow
    {
        public Vector2 Offset;
        public Color Color;

        public TextWriterDropShadow(Vector2 offset, Color color)
        {
            Offset = offset;
            Color = color;
        }

    }
    public class TextTextureWriter
        {

            GraphicsDevice _graphics;
            SpriteBatch _spriteBatch;

            public TextTextureWriter(GraphicsDevice graphics)
            {
                _graphics = graphics;
                _spriteBatch = new SpriteBatch(graphics);
            }

            public Texture2D CreateTextSpriteTexture(SpriteFont spriteFont, string textString)
            {
                return CreateTextSpriteTexture(spriteFont, textString, Color.White, null);
            }

            public Texture2D CreateTextSpriteTexture(SpriteFont spriteFont, string textString, Color textColor, List<TextWriterDropShadow> dropShadows)
            {
                return RenderTextTexture(spriteFont, textString, textColor, null, "", 1.0f, Vector2.Zero, dropShadows);
            }

            public Texture2D CreateButtonGuideTextSpriteTexture(SpriteFont textSpriteFont, SpriteFont buttonGuideSpriteFont, string textString, string buttonGuideText, float buttonGuideScale, Vector2 buttonGuidePositionOffset)
            {
                return CreateButtonGuideTextSpriteTexture(textSpriteFont, buttonGuideSpriteFont, textString, buttonGuideText, buttonGuideScale, buttonGuidePositionOffset, Color.White, null);
            }

            public Texture2D CreateButtonGuideTextSpriteTexture(SpriteFont textSpriteFont, SpriteFont buttonGuideSpriteFont, string textString, string buttonGuideText, float buttonGuideScale, Vector2 buttonGuidePositionOffset, Color textColor, List<TextWriterDropShadow> dropShadows)
            {
                return RenderTextTexture(textSpriteFont, textString, textColor, buttonGuideSpriteFont, buttonGuideText, buttonGuideScale, buttonGuidePositionOffset, dropShadows);
            }

            private Texture2D RenderTextTexture(SpriteFont textSpriteFont, string textString, Color textColor, SpriteFont buttonGuideSpriteFont, string buttonGuideText, float buttonGuideScale, Vector2 buttonGuidePositionOffset, List<TextWriterDropShadow> dropShadows)
            {
                if (textSpriteFont != null)
                {
                    Vector2 stringDimensions = textSpriteFont.MeasureString(textString);

                    float buttonHorizontalShift = 0.0f;
                    Vector2 buttonTextDimensions = Vector2.Zero;
                    if (buttonGuideSpriteFont != null)
                    {
                        buttonTextDimensions = buttonGuideSpriteFont.MeasureString(buttonGuideText);
                        buttonTextDimensions.X *= buttonGuideScale;

                        buttonTextDimensions.X += Math.Abs(buttonGuidePositionOffset.X);
                        if (buttonGuidePositionOffset.X < 0.0f)
                        {
                            buttonHorizontalShift = -buttonGuidePositionOffset.X;
                        }

                        //Eliminate potential non-integral values that could result in lost pixels on texture render
                        buttonTextDimensions.X = (float)Math.Ceiling(buttonTextDimensions.X);
                    }

                    Vector2 dropShadowPositionAdjust = Vector2.Zero;
                    Vector2 dropShadowSizeAdjust = Vector2.Zero;
                    if (dropShadows != null)
                    {
                        Vector2 lowestVals = Vector2.Zero;
                        Vector2 hightestVals = Vector2.Zero;
                        foreach (TextWriterDropShadow dropShadow in dropShadows)
                        {
                            if (dropShadow.Offset.X < 0.0f)
                                lowestVals.X = Math.Min(lowestVals.X, dropShadow.Offset.X);

                            if (dropShadow.Offset.Y < 0.0f)
                                lowestVals.Y = Math.Min(lowestVals.Y, dropShadow.Offset.Y);

                            if (dropShadow.Offset.X > 0.0f)
                                hightestVals.X = Math.Max(hightestVals.X, dropShadow.Offset.X);

                            if (dropShadow.Offset.Y > 0.0f)
                                hightestVals.Y = Math.Max(hightestVals.Y, dropShadow.Offset.Y);
                        }

                        dropShadowPositionAdjust.X = Math.Abs(lowestVals.X);
                        dropShadowPositionAdjust.Y = Math.Abs(lowestVals.Y);

                        dropShadowSizeAdjust.X = Math.Abs(lowestVals.X) + Math.Abs(hightestVals.X);
                        dropShadowSizeAdjust.Y = Math.Abs(lowestVals.Y) + Math.Abs(hightestVals.Y);
                    }

                    RenderTarget2D renderTarget = new RenderTarget2D(_graphics, (int)stringDimensions.X + (int)buttonTextDimensions.X + (int)dropShadowSizeAdjust.X, (int)stringDimensions.Y + (int)dropShadowSizeAdjust.Y);

                    _graphics.SetRenderTarget(renderTarget);
                    _graphics.Clear(Color.Transparent);
                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                    if (buttonGuideSpriteFont != null)
                    {
                        _spriteBatch.DrawString(buttonGuideSpriteFont, buttonGuideText, dropShadowPositionAdjust + new Vector2(buttonHorizontalShift, buttonTextDimensions.Y * -0.25f * buttonGuideScale) + buttonGuidePositionOffset, Color.White, 0.0f, Vector2.Zero, buttonGuideScale, SpriteEffects.None, 1.0f);
                    }

                    Vector2 textPosition = dropShadowPositionAdjust + new Vector2(buttonHorizontalShift + buttonTextDimensions.X, 0.0f);
                    if (dropShadows != null)
                    {
                        foreach (TextWriterDropShadow dropShadow in dropShadows)
                        {
                            _spriteBatch.DrawString(textSpriteFont, textString, textPosition + dropShadow.Offset, dropShadow.Color);
                        }
                    }

                    _spriteBatch.DrawString(textSpriteFont, textString, textPosition, textColor);

                    _spriteBatch.End();
                    _graphics.SetRenderTarget(null);

                    return renderTarget;
                }
                return null;
            }
        }
    }

    /* Button Guide Mapping
    Character   Image
    Space       Left Thumbstick
    !           Directional Pad
    "           Right Thumbstick
    #           BACK
    $           Guide
    %           START
    &           X
    '           A
    (           Y
    )           B
    *           Right Shoulder
    +           Right Trigger
    ,           Left Trigger
    -           Left Shoulder
    */


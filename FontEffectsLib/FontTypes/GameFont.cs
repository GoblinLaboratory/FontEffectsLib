﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FontEffectsLib.CoreTypes;
using Microsoft.Xna.Framework.Input;

namespace FontEffectsLib.FontTypes
{
    public class GameFont : BaseGameObject
    {
        protected SpriteFont _font;
        protected StringBuilder _text;

        //For efficiency, hitbox is only updated in property get. Sub-classes must access this value via the property
        private RectangleF _hitBox;

        /// <summary>
        /// Gets the bounding box for this GameFont. Supports floating point values.
        /// </summary>
        public RectangleF HitBox
        {
            get
            {
                _hitBox.X = _position.X - _origin.X;
                _hitBox.Y = _position.Y - _origin.Y;
                _hitBox.Width = Size.X;
                _hitBox.Height = Size.Y;
                return _hitBox;
            }
        }

        public virtual SpriteFont Font
        {
            get { return _font; }
            set { _font = value; }
        }

        public virtual StringBuilder Text
        {
            get { return _text; }
        }

        public GameFont(SpriteFont font, Vector2 position, Color tintColor)
            : this(font, String.Empty, position, tintColor)
        {
            //Pass-through constructor
        }

        public GameFont(SpriteFont font, String text, Vector2 position, Color tintColor)
            : base(position, tintColor)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }
            if (text == null)
            {
                //Null string = assume empty string, should I throw ArgumentNullException instead?
                text = String.Empty;
            }

            _font = font;
            _text = new StringBuilder(text);
            _hitBox = new RectangleF(position.X - _origin.X, position.Y - _origin.Y, Size.X, Size.Y);
        }

        public override void SetCenterAsOrigin()
        {
            if (_text.Length > 0)
            {
                _origin = _font.MeasureString(_text) * _scale / 2;
            }
        }

        public virtual Vector2 Size
        {
            get { return _font.MeasureString(_text) * _scale; }
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_isVisible && _text != null)
            {
                spriteBatch.DrawString(_font, _text, _position, _tintColor, _rotation, _origin, _scale, _effects, _layerDepth);
            }
        }
    }
}

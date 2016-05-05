/* Author:      Shawn
 * Date:        5/14/2015 10:42:22 PM
 * Project:     
 * Description:
 *  
 */

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeartOfDarkness.Dialogue
{
    public class TextureChangeTag
    {
        string m_textureName;
        TextureRole m_role;

        public string TextureName
        {
            get { return m_textureName; }
            set { m_textureName = value; }
        }
        public TextureRole Role
        {
            get { return m_role; }
            set { m_role = value; }
        }

        public TextureChangeTag()
        {
            m_textureName = null;
            m_role = TextureRole.Background;
        }

        public Texture2D GetTexture()
        {
            if (TextureName == null)
                return null;
            else
                return Global.Textures[TextureName];
        }
    }

    public enum TextureRole
    {
        Speaker0,
        Speaker1,
        Speaker2,
        Speaker3,
        Speaker4,
        Speaker5,
        Speaker6,
        Speaker7,
        Background,
        TextPanel
    }
}

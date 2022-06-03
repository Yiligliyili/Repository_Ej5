using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Diagnostics;
using TCEngine;

namespace TCGame
{
    public class TargetComponent : BaseComponent
    {
        public TargetComponent()
        {
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.Update;
        }

        public override object Clone()
        {
            TargetComponent clonedComponent = new TargetComponent();
            return clonedComponent;
        }
    }
}

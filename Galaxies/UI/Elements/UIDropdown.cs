using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Galaxies.UI.Elements
{

    class UIDropdown : UIGroup
    {

        public List<DropdownOption> Options { get; protected set; } = new List<DropdownOption>();

        UIButton      optionButton;
        UIFixedColumn optionsColumn;

        public UIDropdown(Transform transform, Screen screen, int defaultOptionIndex = 0, params DropdownOption[] options) : base(transform, null, screen)
        {
            //Button to open the dropdown as well as the text displaying the current selected option:
            optionButton = AddUIElement(new UIButton(
                new Transform(transform.Size),
                ContentHelper.Arial_Font,
                options.Length > defaultOptionIndex ? options[defaultOptionIndex].Title : "No options",
                TextAlign.MiddleLeft,
                5,
                ContentHelper.GetSprite("Sprites/UI/column"),
                new EventArg0(ToggleUIOptionsColumn),
                screen
                ));

            //Options (in a fixed column):
            optionsColumn = AddUIElement(new UIFixedColumn(
                new Transform(new Vector2(0, transform.Size.Y), transform.Size),
                null,
                screen,
                Vector4.Zero,
                Vector2.Zero
                ));

            optionsColumn.Visable = false;

            //Dropdown arrow:
            AddUIElement(new UIElement(
                new Transform(new Vector2(transform.Width / 2f - transform.Height / 2f, 0), new Vector2(transform.Height, transform.Height)),
                ContentHelper.GetSprite("Sprites/UI/column"),
                screen
                ));

            Options.AddRange(options);

            foreach (var option in options)
                CreateOption(option);

            if (options.Length > defaultOptionIndex)
                OptionChosen(options[defaultOptionIndex]);
        }

        #region Managing options

        public void AddOption(DropdownOption option)
        {
            Options.Add(option);
            CreateOption(option);
        }

        public void SelectOption(DropdownOption option)
        {
            if (Options.Contains(option))
            {
                OptionChosen(option);
            }
        }

        private void CreateOption(DropdownOption option)
        {
            option.OnChoose = new EventArgList(option.OnChoose, new EventArg1<DropdownOption>(OptionChosen, option));

            option.UIButton = optionsColumn.AddUIElement(new UIButton(
                new Transform(new Vector2(transform.Width, 50)),
                ContentHelper.Arial_Font,
                option.Title,
                TextAlign.MiddleLeft,
                5,
                ContentHelper.GetSprite("Sprites/UI/column"),
                option.OnChoose,
                screen
                ));
        }

        private void OptionChosen(DropdownOption option)
        {
            optionButton.Text = option.Title;

            optionsColumn.Visable = false;
        }
        
        #endregion

        private void ToggleUIOptionsColumn()
        {
            optionsColumn.Visable = !optionsColumn.Visable;
        }

    }

    class DropdownOption
    {

        public string   Title    { get; private set; }
        public EventArg OnChoose { get; set; }
        public UIButton UIButton { get; set; }

        public DropdownOption(string title, EventArg onChoose)
        {
            this.Title    = title;
            this.OnChoose = onChoose;
        }

    }

}

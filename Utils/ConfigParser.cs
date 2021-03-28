using System;
using System.Windows.Forms;

namespace MagickUtils.Utils
{
    class ConfigParser
    {
		public enum StringMode { Any, Int, Float }

		public static void SaveGuiElement(TextBox textbox, StringMode stringMode = StringMode.Any)
		{
			switch (stringMode)
			{
				case StringMode.Any: global::MagickUtils.Config.Set(textbox.Name, textbox.Text); break;
				case StringMode.Int: global::MagickUtils.Config.Set(textbox.Name, textbox.Text.GetInt().ToString()); break;
				case StringMode.Float: global::MagickUtils.Config.Set(textbox.Name, textbox.Text.GetFloat().ToString()); break;
			}
		}

		public static void SaveGuiElement(ComboBox comboBox, StringMode stringMode = StringMode.Any)
		{
			switch (stringMode)
			{
				case StringMode.Any: global::MagickUtils.Config.Set(comboBox.Name, comboBox.Text); break;
				case StringMode.Int: global::MagickUtils.Config.Set(comboBox.Name, comboBox.Text.GetInt().ToString()); break;
				case StringMode.Float: global::MagickUtils.Config.Set(comboBox.Name, comboBox.Text.GetFloat().ToString().Replace(",", ".")); break;
			}
		}

		public static void SaveGuiElement(CheckBox checkbox)
		{
			global::MagickUtils.Config.Set(checkbox.Name, checkbox.Checked.ToString());
		}

		public static void SaveGuiElement(NumericUpDown upDown, StringMode stringMode = StringMode.Any)
		{
			switch (stringMode)
			{
				case StringMode.Any: global::MagickUtils.Config.Set(upDown.Name, ((float)upDown.Value).ToString().Replace(",", ".")); break;
				case StringMode.Int: global::MagickUtils.Config.Set(upDown.Name, ((int)upDown.Value).ToString()); break;
				case StringMode.Float: global::MagickUtils.Config.Set(upDown.Name, ((float)upDown.Value).ToString().Replace(",", ".")); ; break;
			}
		}

		public static void SaveComboxIndex(ComboBox comboBox)
		{
			global::MagickUtils.Config.Set(comboBox.Name, comboBox.SelectedIndex.ToString());
		}

		public static void LoadGuiElement(ComboBox comboBox, string suffix = "")
		{
			comboBox.Text = global::MagickUtils.Config.Get(comboBox.Name) + suffix;
		}

		public static void LoadGuiElement(TextBox textbox, string suffix = "")
		{
			textbox.Text = global::MagickUtils.Config.Get(textbox.Name) + suffix; ;
		}

		public static void LoadGuiElement(CheckBox checkbox)
		{
			checkbox.Checked = global::MagickUtils.Config.GetBool(checkbox.Name);
		}

		public static void LoadGuiElement(NumericUpDown upDown)
		{
			upDown.Value = Convert.ToDecimal(global::MagickUtils.Config.GetFloat(upDown.Name));
		}

		public static void LoadComboxIndex(ComboBox comboBox)
		{
			comboBox.SelectedIndex = global::MagickUtils.Config.GetInt(comboBox.Name);
		}
	}
}

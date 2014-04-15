using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace SingleDecimalPointBehavior
{
    public static class DecimaPointBehavior
    {
        // Using a DependencyProperty as the backing store for IsDecimalPointAllowed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDecimalPointAllowedProperty =
            DependencyProperty.RegisterAttached("IsDecimalPointAllowed", typeof(bool?), typeof(DecimaPointBehavior), new PropertyMetadata(null, OnIsDecimalPointAllowedChanged));

        private static string regex = @"^([0-9]+)?$";

        public static bool? GetIsDecimalPointAllowed(DependencyObject obj)
        {
            return (bool?)obj.GetValue(IsDecimalPointAllowedProperty);
        }

        public static void SetIsDecimalPointAllowed(DependencyObject obj, bool? value)
        {
            obj.SetValue(IsDecimalPointAllowedProperty, value);
        }

        private static void OnIsDecimalPointAllowedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var isDecimalPointAllowed = (bool)(e.NewValue);
            var textBox = (TextBox)sender;

            regex = @"^([0-9]+)?$";
            if (isDecimalPointAllowed)
            {
                regex = @"^([0-9]+)?([,|\.])?([0-9]+)?$";
            }

            textBox.TextChanged += textBox_TextChanged;
        }

        private static void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var match = Regex.Match(textBox.Text, regex);
            if (!match.Success)
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                textBox.Select(textBox.Text.Length, 0);
            }
        }
    }
}
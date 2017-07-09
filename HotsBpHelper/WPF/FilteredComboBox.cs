﻿using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using HotsBpHelper.Utils.ComboBoxItemUtil;

namespace HotsBpHelper.WPF
{
    public class FilteredComboBox : ComboBox
    {
        private string oldFilter = string.Empty;

        private string currentFilter = string.Empty;

        protected TextBox EditableTextBox => GetTemplateChild("PART_EditableTextBox") as TextBox;

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            Effect = null;
            base.OnSelectionChanged(e);
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue != null)
            {
                var view = CollectionViewSource.GetDefaultView(newValue);
                view.Filter += FilterItem;
            }

            if (oldValue != null)
            {
                var view = CollectionViewSource.GetDefaultView(oldValue);
                if (view != null) view.Filter -= FilterItem;
            }

            base.OnItemsSourceChanged(oldValue, newValue);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            string temp = Text;
            switch (e.Key)
            {
                case Key.Tab:
                case Key.Enter:
                    if (SelectedIndex == -1) SelectedIndex = 0;
                    IsDropDownOpen = false;
                    break;
                case Key.Escape:
                    IsDropDownOpen = false;
                    SelectedIndex = -1;
                    Text = currentFilter;
                    break;
                case Key.Down:
                    IsDropDownOpen = true;
                    if (SelectedIndex == -1) SelectedIndex = 0;
                    base.OnPreviewKeyDown(e);
                    break;
                case Key.Up:
                    IsDropDownOpen = true;
                    if (SelectedIndex == -1) SelectedIndex = 0;
                    base.OnPreviewKeyDown(e);
                    break;

                default:

                    if (e.Key == Key.Down) IsDropDownOpen = true;
                    base.OnPreviewKeyDown(e);
                    break;
            }

            // Cache text
            oldFilter = Text;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                case Key.Down:
                    base.OnKeyUp(e);
                    break;
                case Key.Tab:
                case Key.Enter:

                    ClearFilter();
                    base.OnKeyUp(e);
                    break;
                default:
                    if (Text != oldFilter)
                    {
                        RefreshFilter();
                        IsDropDownOpen = true;

                        EditableTextBox.SelectionStart = int.MaxValue;
                    }

                    base.OnKeyUp(e);
                    currentFilter = Text;
                    break;
            }
        }
        /*
                protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
                {
                    ClearFilter();
                    var temp = SelectedIndex;
                    SelectedIndex = -1;
                    Text = string.Empty;
                    SelectedIndex = temp;
                    base.OnPreviewLostKeyboardFocus(e);
                }
        */


        private void RefreshFilter()
        {
            if (ItemsSource == null) return;

            var view = CollectionViewSource.GetDefaultView(ItemsSource);
            view.Refresh();
        }

        private void ClearFilter()
        {
            currentFilter = string.Empty;
            RefreshFilter();
        }

        private bool FilterItem(object value)
        {
            if (value == null) return false;
            if (Text.Length == 0) return true;

            return value.ToString().ToLower().Contains(Text.ToLower());
        }
    }
}
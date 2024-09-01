﻿namespace CanonUtility.Behavior;

public class BindableSelectedItemBehavior : Behavior<TreeView>
{
    public object SelectedItem
    {
        get => (object)GetValue(SelectedItemProperty); 
        set => SetValue(SelectedItemProperty, value); 
    }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(object), typeof(BindableSelectedItemBehavior), new UIPropertyMetadata(null, OnSelectedItemChanged));

    private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is TreeViewItem item)
        {
            item.SetValue(TreeViewItem.IsSelectedProperty, true);
        }
    }

    protected override void OnAttached()
    {
        base.OnAttached();

        this.AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (this.AssociatedObject != null)
        {
            this.AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
        }
    }

    private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        this.SelectedItem = e.NewValue;
    }
}
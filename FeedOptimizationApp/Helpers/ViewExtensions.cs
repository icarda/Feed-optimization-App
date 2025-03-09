namespace FeedOptimizationApp.Helpers;

public static class ViewExtensions
{
    // Extension method to set the row and column of a view in a Grid
    public static TView SetGrid<TView>(this TView view, int row, int column) where TView : View
    {
        Grid.SetRow(view, row);
        Grid.SetColumn(view, column);
        return view;
    }
}
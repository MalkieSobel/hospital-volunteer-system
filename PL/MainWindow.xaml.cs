using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public static readonly DependencyProperty CurrentTimeProperty =
        DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow));
   
    

    public DateTime CurrentTime
    {
        get { return (DateTime)GetValue(CurrentTimeProperty); }
        set { SetValue(CurrentTimeProperty, value); }
    }


    private void btnAddOneMinute_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ClockPromotion(BO.TimeUnit.Minute);
    }

    private void btnAddOnehour_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ClockPromotion(BO.TimeUnit.Hour);
    }

    private void btnAddOneday_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ClockPromotion(BO.TimeUnit.Day);
    }

    private void btnAddOneyear_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ClockPromotion(BO.TimeUnit.Month);
    }


    public MainWindow()
    {
        //InitializeComponent();
    }
}
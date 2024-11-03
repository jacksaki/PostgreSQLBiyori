using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModernWpf.Controls;

namespace PostgreSQLBiyori.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static IReadOnlyDictionary<ViewTypes, Type> _pages = new Dictionary<ViewTypes, Type>()
        {
            {ViewTypes.Objects, typeof(ObjectsView)},
            {ViewTypes.ExplainPlan, typeof(ExplainPlanView)},
            {ViewTypes.Settings, typeof(SettingsView)},
            {ViewTypes.EFCore, typeof(EFCoreView)},
            // 空ページ
            {ViewTypes.None, typeof(BrankPage)},
        };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NaviView_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            try
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                // Tag取得
                string iconName = selectedItem.Tag?.ToString();
                // ヘッダー設定
                sender.Header = iconName;

                if (Enum.TryParse(iconName, out ViewTypes icon))
                {
                    // 対応するページ表示
                    ContentFrame.Navigate(_pages[icon]);
                }
                else
                {
                    // 空ページ表示
                    ContentFrame.Navigate(_pages[ViewTypes.None]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
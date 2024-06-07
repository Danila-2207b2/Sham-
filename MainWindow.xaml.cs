using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace Project1
{
    public partial class MainWindow : Window
    {
        private int count = 1;
        public static List<_Table> result = new List<_Table>();

        /*
         В этой части определяется класс который является главным окном приложения.
         В конструкторе происходит инициализация компонентов, чтение данных из файла, а также привязка событий для чекбоксов.
         */
        public MainWindow()
        {

            InitializeComponent();
            fileread();
            checkBox1.Checked += CheckBox_Checked;
            checkBox1.Unchecked += CheckBox_Unchecked;

            checkBox2.Checked += CheckBox_Checked;
            checkBox2.Unchecked += CheckBox_Unchecked;

            checkBox3.Checked += CheckBox_Checked;
            checkBox3.Unchecked += CheckBox_Unchecked;
        }

        /*
         метод обрабатывает нажатие кнопки удаления элемента.
         Если пользователь подтверждает удаление, выбранный элемент удаляется из списка, обновляется порядковый номер элементов и данные записываются в файл.
         */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить?", "Вы уверены?",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (TGrid.ItemsSource is IList list) list.Remove(TGrid.SelectedItem);
                count--;
                for (int i = 0; i < result.Count(); i++) result[i].Id = i + 1;
                TGrid.Items.Refresh();
                filewrite();
            }

        }
        /*
         метод обрабатывает нажатие кнопки добавления нового элемента
         */
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Add window = new Add(count);
            window.ShowDialog();
            MainWindow.result.Add(new _Table(count, window._name, window._photo, window._info));
            count++;
            TGrid.ItemsSource = result;
            TGrid.Items.Refresh();
            filewrite();
        }
        /*
         Эти методы отвечают за фильтрацию и поиск данных в списке элементов.
        */
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(TGrid.ItemsSource);
            view.Filter = null;
            view.SortDescriptions.Clear();
            if (checkBox1.IsChecked == true) view.SortDescriptions.Add(new SortDescription("Животное", ListSortDirection.Ascending));
            else if (checkBox2.IsChecked == true) view.SortDescriptions.Add(new SortDescription("Фото", ListSortDirection.Ascending));
            if (checkBox3.IsChecked == true) view.Filter = item => !string.IsNullOrEmpty((item as _Table)?.Info);

            TGrid.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string searchName = search.Text;
            ICollectionView view = CollectionViewSource.GetDefaultView(TGrid.ItemsSource);
            if (view.Filter != null) view.Filter = null;
            else
            {
                if (!string.IsNullOrEmpty(searchName))
                {
                    view.Filter = item =>
                    {
                        _Table table = item as _Table;
                        return table.Name.Contains(searchName);
                    };
                }
            }
            TGrid.Items.Refresh();
        }

        /*
           метод считывает данные из файла "file.txt", разбивает строки на части и добавляет новые элементы в список.
         */
        private void fileread()
        {
            string[] lines = File.ReadAllLines("file.txt");

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                string name = parts[0];
                string photo = parts[1];
                string info = string.Join(" ", parts, 2, parts.Length - 2).Trim();
                result.Add(new _Table(count, name, photo, info));
                count++;
            }
            TGrid.ItemsSource = result;
            TGrid.Items.Refresh();
        }
        /*
            Этот метод записывает содержимое коллекции result в файл.
             Он использует StreamWriter, чтобы открыть файл для записи.
             Затем он перебирает каждый элемент в коллекции результат
         */
        private void filewrite()
        {
            using (StreamWriter writer = new StreamWriter("file.txt"))
            {
                foreach (var item in result) writer.Write($"{item.Name} {item.Photo} {item.Info}\n");

            }
        }

        /*
         метод является обработчиком события контекстного меню.
         */
        private void d(object sender, ContextMenuEventArgs e)
        {
            exp.Background = Brushes.Black;
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CourierTrackerWinForms
{
    public partial class Form1 : Form
    {
        // Объявляем элементы интерфейса
        private TabControl tabControl;
        private TextBox txtName, txtCity, txtEarnings, txtTips;
        private NumericUpDown numAge, numOrders, numHours, numKilometers;
        private ComboBox cmbTransport;
        private RichTextBox rtbResult;

        public Form1()
        {
            // Устанавливаем настройки самого "телефона"
            this.Size = new Size(380, 640);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Courier Tracker";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250); // Светлый аккуратный фон

            // Инициализируем и настраиваем менеджер экранов
            InitializeTabControl();

            // Создаем все 10 экранов программно
            CreateAllPages();
        }

        private void InitializeTabControl()
        {
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Магия скрытия вкладок
            tabControl.Appearance = TabAppearance.Buttons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;

            this.Controls.Add(tabControl);
        }

        private void CreateAllPages()
        {
            // 1. Имя
            txtName = new TextBox { Width = 280, Font = new Font("Segoe UI", 12) };
            AddQuestionPage("Введите ваше имя:", txtName);

            // 2. Возраст
            numAge = new NumericUpDown { Width = 280, Font = new Font("Segoe UI", 12), Minimum = 14, Maximum = 100 };
            AddQuestionPage("Введите ваш возраст:", numAge);

            // 3. Транспорт
            cmbTransport = new ComboBox { Width = 280, Font = new Font("Segoe UI", 12), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTransport.Items.AddRange(new string[] { "Пешком", "Велосипед", "Самокат", "Автомобиль" });
            AddQuestionPage("Укажите ваш тип передвижения:", cmbTransport);

            // 4. Город
            txtCity = new TextBox { Width = 280, Font = new Font("Segoe UI", 12) };
            AddQuestionPage("Укажите ваш город:", txtCity);

            // 5. Заказы
            numOrders = new NumericUpDown { Width = 280, Font = new Font("Segoe UI", 12), Maximum = 1000 };
            AddQuestionPage("Сколько заказов вы выполнили за день?", numOrders);

            // 6. Часы
            numHours = new NumericUpDown { Width = 280, Font = new Font("Segoe UI", 12), Maximum = 24 };
            AddQuestionPage("Сколько часов вы работали?", numHours);

            // 7. Километры
            numKilometers = new NumericUpDown { Width = 280, Font = new Font("Segoe UI", 12), Maximum = 1000 };
            AddQuestionPage("Сколько километров вы проехали?", numKilometers);

            // 8. Заработок
            txtEarnings = new TextBox { Width = 280, Font = new Font("Segoe UI", 12) };
            AddQuestionPage("Сколько вы сегодня заработали?", txtEarnings);

            // 9. Чаевые (Финальный вопрос)
            txtTips = new TextBox { Width = 280, Font = new Font("Segoe UI", 12) };
            AddQuestionPage("Сколько чаевых вы получили?", txtTips, isFinalQuestion: true);

            // 10. Экран результатов
            CreateResultPage();
        }

        // Универсальный шаблон создания экрана с вопросом
        private void AddQuestionPage(string questionText, Control inputControl, bool isFinalQuestion = false)
        {
            TabPage page = new TabPage { BackColor = this.BackColor };

            // Текст вопроса
            Label lbl = new Label
            {
                Text = questionText,
                Location = new Point(40, 120),
                Size = new Size(280, 80),
                Font = new Font("Segoe UI Semibold", 14),
                TextAlign = ContentAlignment.BottomLeft
            };

            // Позиция поля ввода
            inputControl.Location = new Point(40, 220);

            // Кнопка перехода
            Button btn = new Button
            {
                Text = isFinalQuestion ? "Показать результат" : "Далее",
                Location = new Point(40, 300),
                Size = new Size(280, 45),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 255), // Красивый синий цвет (iOS style)
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderSize = 0;

            // Привязка логики клика
            if (isFinalQuestion)
                btn.Click += BtnFinal_Click;
            else
                btn.Click += (s, e) => tabControl.SelectedIndex += 1;

            page.Controls.Add(lbl);
            page.Controls.Add(inputControl);
            page.Controls.Add(btn);
            tabControl.TabPages.Add(page);
        }

        // Объявляем новые элементы для красивого экрана итогов
        private Label lblResultHeader, lblTotalMoney, lblStatsLeft, lblStatsRight, lblEncouragement;
        private Panel pbxHeaderCard, pbxStatsCard;

        private void CreateResultPage()
        {
            TabPage page = new TabPage { BackColor = this.BackColor };

            // 1. Верхняя карточка — Главный заработок
            pbxHeaderCard = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(325, 130),
                BackColor = Color.FromArgb(0, 122, 255), // Яркий синий бренд-цвет
            };
            // Скругление углов и стилизация (визуальный трюк для WinForms)
            lblResultHeader = new Label
            {
                Text = "Имя, отличная работа!",
                Location = new Point(15, 15),
                Size = new Size(295, 25),
                Font = new Font("Segoe UI Semibold", 12),
                ForeColor = Color.White
            };
            lblTotalMoney = new Label
            {
                Text = "0.00 ₽",
                Location = new Point(15, 45),
                Size = new Size(295, 60),
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };
            pbxHeaderCard.Controls.Add(lblResultHeader);
            pbxHeaderCard.Controls.Add(lblTotalMoney);

            // 2. Нижняя карточка — Детальная статистика
            pbxStatsCard = new Panel
            {
                Location = new Point(20, 165),
                Size = new Size(325, 260),
                BackColor = Color.White,
            };

            // Левая колонка метрик (Транспорт, Заказы, Время)
            lblStatsLeft = new Label
            {
                Location = new Point(15, 20),
                Size = new Size(140, 220),
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(100, 110, 120),
                AutoSize = false,
                Padding = new Padding(0, 0, 0, 6) // Добавляем отступ снизу вместо недоступного LineSpacing
            };

            // Правая колонка метрик (Город, Километры, Прочее)
            lblStatsRight = new Label
            {
                Location = new Point(165, 20),
                Size = new Size(145, 220),
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(100, 110, 120),
            };
            pbxStatsCard.Controls.Add(lblStatsLeft);
            pbxStatsCard.Controls.Add(lblStatsRight);

            // 3. Блок мотивации (Снизу)
            lblEncouragement = new Label
            {
                Text = "Так держать!\nСпасибо за использование Courier Tracker!",
                Location = new Point(20, 440),
                Size = new Size(325, 50),
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.FromArgb(140, 150, 160),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // 4. Кнопка перезапуска (Широкая, навигационная)
            Button btnRestart = new Button
            {
                Text = "🔄  Начать новый день",
                Location = new Point(20, 505),
                Size = new Size(325, 48),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113), // Приятный зеленый
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRestart.FlatAppearance.BorderSize = 0;
            btnRestart.Click += BtnRestart_Click;

            // Добавляем все элементы на страницу
            page.Controls.Add(pbxHeaderCard);
            page.Controls.Add(pbxStatsCard);
            page.Controls.Add(lblEncouragement);
            page.Controls.Add(btnRestart);

            tabControl.TabPages.Add(page);
        }


        // Логика расчета (Твой код из консоли)
        private void BtnFinal_Click(object sender, EventArgs e)
        {
            // Получаем данные из полей
            string name = string.IsNullOrWhiteSpace(txtName.Text) ? "Курьер" : txtName.Text;
            int age = (int)numAge.Value;
            string transportType = cmbTransport.SelectedItem?.ToString() ?? "Не указан";
            string city = string.IsNullOrWhiteSpace(txtCity.Text) ? "Не указан" : txtCity.Text;
            int orders = (int)numOrders.Value;
            int hours = (int)numHours.Value;
            int kilometers = (int)numKilometers.Value;

            decimal earnings = decimal.TryParse(txtEarnings.Text, out decimal earn) ? earn : 0;
            decimal tips = decimal.TryParse(txtTips.Text, out decimal tip) ? tip : 0;
            decimal totalEarnings = earnings + tips;

            // 1. Динамический заголовок
            lblResultHeader.Text = $"{name}, отличная работа сегодня!";

            // 2. Огромный итоговый баланс
            lblTotalMoney.Text = $"{totalEarnings:N2} ₽";

            // 3. Формируем левую колонку (Финансы и активность)
            lblStatsLeft.Text =
                $"💰 Заработок:\n" +
                $"{earnings:N0} руб.\n\n" +
                $"🎁 Чаевые:\n" +
                $"{tips:N0} руб.\n\n" +
                $"📦 Заказы:\n" +
                $"{orders} шт.";

            // 4. Формируем правую колонку (Логистика и профиль)
            lblStatsRight.Text =
                $"📍 Город:\n" +
                $"{city}\n\n" +
                $"🚲 Транспорт:\n" +
                $"{transportType}\n\n" +
                $"⏱ В пути: {hours} ч.\n" +
                $"🛣 Пробег: {kilometers} км";

            // Переключаемся на финальный экран
            tabControl.SelectedIndex = 9;
        }


        private void BtnRestart_Click(object sender, EventArgs e)
        {
            txtName.Clear(); txtCity.Clear(); txtEarnings.Clear(); txtTips.Clear();
            numAge.Value = 14; numOrders.Value = 0; numHours.Value = 0; numKilometers.Value = 0;
            cmbTransport.SelectedIndex = -1;
            tabControl.SelectedIndex = 0;
        }
    }
}

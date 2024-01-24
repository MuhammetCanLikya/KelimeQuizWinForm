namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> words; // Ýngilizce kelimelerin ve Türkçe karþýlýklarýnýn saklandýðý sözlük
        private Random random;
        private KeyValuePair<string, string> currentWordPair;

        public Form1()
        {
            InitializeComponent();
            InitializeWords();
            random = new Random();
            DisplayRandomWord();
        }

        private void InitializeWords()
        {
            // Kelimeleri sözlüðe ekleyin (istediðiniz kadar kelime ekleyebilirsiniz)
            words = new Dictionary<string, string>
            {
                {"Hello", "Merhaba"},
                {"Goodbye", "Hoþça kal"},
                {"Dog", "Köpek"},
                {"Cat", "Kedi"},
                // ... diðer kelimeler
            };
        }

        private void DisplayRandomWord()
        {
            // Rastgele bir kelime çekin
            int randomIndex = random.Next(words.Count);
            currentWordPair = words.ElementAt(randomIndex);

            // Ýngilizce kelimeyi ekrana yazdýrýn
            label1.Text = currentWordPair.Key;

            // Butonlara rastgele kelimeleri ayarla
            List<string> randomWords = GetRandomWords(currentWordPair.Value);
            button1.Text = randomWords[0];
            button2.Text = randomWords[1];
            button3.Text = randomWords[2];
            button4.Text = randomWords[3];


        }


        private List<string> GetRandomWords(string correctWord)
        {
            List<string> randomWords = new List<string> { correctWord };

            // Diðer rastgele kelimeleri seçin (bu kýsmý özelleþtirebilirsiniz)
            while (randomWords.Count < 4)
            {
                int randomIndex = random.Next(words.Count);
                string randomWord = words.ElementAt(randomIndex).Value;

                // Ayný kelimenin tekrar seçilmesini önleyin
                if (!randomWords.Contains(randomWord))
                {
                    randomWords.Add(randomWord);
                }
            }

            // Karýþtýrýn
            randomWords = randomWords.OrderBy(x => Guid.NewGuid()).ToList();

            return randomWords;
        }



        public void wordCheck(string word)
        {
            string userAnswer = word;
            string correctAnswer = currentWordPair.Value;
            if (userAnswer == correctAnswer)
            {
                MessageBox.Show("Doðru!");
            }
            else
            {
                MessageBox.Show("Yanlýþ. Doðru cevap: " + currentWordPair.Value);
            }
        }
            private void button1_Click(object sender, EventArgs e)
        {
            wordCheck(button1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wordCheck(button2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            wordCheck(button4.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            wordCheck(button3.Text);
        }
    }
}

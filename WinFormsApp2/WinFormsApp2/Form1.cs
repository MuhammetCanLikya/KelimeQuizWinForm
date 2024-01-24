namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> words; // �ngilizce kelimelerin ve T�rk�e kar��l�klar�n�n sakland��� s�zl�k
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
            // Kelimeleri s�zl��e ekleyin (istedi�iniz kadar kelime ekleyebilirsiniz)
            words = new Dictionary<string, string>
            {
                {"Hello", "Merhaba"},
                {"Goodbye", "Ho��a kal"},
                {"Dog", "K�pek"},
                {"Cat", "Kedi"},
                // ... di�er kelimeler
            };
        }

        private void DisplayRandomWord()
        {
            // Rastgele bir kelime �ekin
            int randomIndex = random.Next(words.Count);
            currentWordPair = words.ElementAt(randomIndex);

            // �ngilizce kelimeyi ekrana yazd�r�n
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

            // Di�er rastgele kelimeleri se�in (bu k�sm� �zelle�tirebilirsiniz)
            while (randomWords.Count < 4)
            {
                int randomIndex = random.Next(words.Count);
                string randomWord = words.ElementAt(randomIndex).Value;

                // Ayn� kelimenin tekrar se�ilmesini �nleyin
                if (!randomWords.Contains(randomWord))
                {
                    randomWords.Add(randomWord);
                }
            }

            // Kar��t�r�n
            randomWords = randomWords.OrderBy(x => Guid.NewGuid()).ToList();

            return randomWords;
        }



        public void wordCheck(string word)
        {
            string userAnswer = word;
            string correctAnswer = currentWordPair.Value;
            if (userAnswer == correctAnswer)
            {
                MessageBox.Show("Do�ru!");
            }
            else
            {
                MessageBox.Show("Yanl��. Do�ru cevap: " + currentWordPair.Value);
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

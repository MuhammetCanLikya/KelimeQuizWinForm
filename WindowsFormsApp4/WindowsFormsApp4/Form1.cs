using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> words; // İngilizce kelimelerin ve Türkçe karşılıklarının saklandığı sözlük

        //değişik dal
        private Random random;
        private KeyValuePair<string, string> currentWordPair;

        public Form1()
        {
            InitializeComponent();
            InitializeWords();
            InitializeWordsWithAccessDatabase();
            random = new Random();
            DisplayRandomWord();
        }

        private void InitializeWords()
        {
            // Kelimeleri sözlüğe ekleyin (istediğiniz kadar kelime ekleyebilirsiniz)
            words = new Dictionary<string, string>
            {
                
            };
        }

        private void InitializeWordsWithAccessDatabase()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=kelimeler.accdb;Persist Security Info=False;";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT englishword, turkishword FROM tlbKelimeler"; // tblKelimeler, veritabanındaki tablonun adını temsil etmelidir.

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string englishWord = reader["englishword"].ToString();
                            string turkishWord = reader["turkishword"].ToString();

                            // Veritabanından alınan kelimeleri sözlüğe ekle
                            if (!words.ContainsKey(englishWord))
                            {
                                words.Add(englishWord, turkishWord);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda işlemleriniz
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
            }
        }


            private void DisplayRandomWord()
            {
            // Rastgele bir kelime çekin
            int randomIndex = random.Next(words.Count);
            currentWordPair = words.ElementAt(randomIndex);

            // İngilizce kelimeyi ekrana yazdırın
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

            // Diğer rastgele kelimeleri seçin (bu kısmı özelleştirebilirsiniz)
            while (randomWords.Count < 4)
            {
                int randomIndex = random.Next(words.Count);
                string randomWord = words.ElementAt(randomIndex).Value;

                // Aynı kelimenin tekrar seçilmesini önleyin
                if (!randomWords.Contains(randomWord))
                {
                    randomWords.Add(randomWord);
                }
            }

            // Karıştırın
            randomWords = randomWords.OrderBy(x => Guid.NewGuid()).ToList();

            return randomWords;
        }

        public void wordCheck(string word)
        {
            string userAnswer = word;
            string correctAnswer = currentWordPair.Value;
            if (userAnswer == correctAnswer)
            {
                MessageBox.Show("Doğru!");
            }
            else
            {
                MessageBox.Show("Yanlış. Doğru cevap: " + currentWordPair.Value);
            }
        }
        public void difficultyCheck(string difficultyAnswer)
        {
            if (difficultyAnswer == "Kolay")
            { }
            else if (difficultyAnswer == "Orta")
            { }
            else if (difficultyAnswer == "Zor")
            { }
        }
        public void UpdateDifficulty(string wordToUpdate, string newDifficulty)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=kelimeler.accdb;Persist Security Info=False;";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // UPDATE SQL sorgusunu oluştur
                    string updateQuery = "UPDATE tlbKelimeler SET difficulty = @newDifficulty WHERE englishword = @wordToUpdate";

                    using (OleDbCommand updateCommand = new OleDbCommand(updateQuery, connection))
                    {
                        // Parametreleri ekleyin
                        updateCommand.Parameters.AddWithValue("@newDifficulty", newDifficulty);
                        updateCommand.Parameters.AddWithValue("@wordToUpdate", wordToUpdate);

                        // SQL sorgusunu çalıştır
                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Güncelleme başarılı.");
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme yapılamadı. Belirtilen kelime bulunamadı.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
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

        private void button5_Click(object sender, EventArgs e)
        {
            DisplayRandomWord();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateDifficulty(label1.Text, "Kolay");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UpdateDifficulty(label1.Text, "Orta");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UpdateDifficulty(label1.Text, "Zor");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            UpdateDifficulty(label1.Text, "Tanımlanmamış");
        }
    }//class sonu
}//namespace sonu


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



public class TextFiles : IEquatable<TextFiles>, IComparable<TextFiles>
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public int Size { get; set; }
    public string Content { get; set; }
    public string FullSize { get; set; }
    public TextFiles(string name, string Extension, string size, string content)
    {
        char[] delimiters = { 'K', 'B', 'M', 'G' };
        this.Name = name;
        this.Extension = Extension;
        this.FullSize = size;
        this.Size = Int32.Parse(size.TrimEnd(delimiters));
        this.Content = content;
    }
    public int CompareTo(TextFiles comparePart)
    {
        if (comparePart == null)
            return 1;

        else
            return this.Size.CompareTo(comparePart.Size);
    }
    public bool Equals(TextFiles other)
    {
        if (other == null) return false;
        return (this.Size.Equals(other.Size));
    }
}

public class Movies : IEquatable<Movies>, IComparable<Movies>
{
    public string Name;
    public string Extension;
    public int Size;
    public string Resolution;
    public string Lenght;
    public string FullSize { get; set; }
    public Movies(string name, string Extension, string size, string resolution, string length)
    {
        char[] delimiters = { 'K', 'B', 'M', 'G' };
        this.Name = name;
        this.Extension = Extension;
        this.FullSize = size;
        this.Size = Int32.Parse(size.TrimEnd(delimiters));
        this.Resolution = resolution;
        this.Lenght = length;
    }
    public int CompareTo(Movies comparePart)
    {
        if (comparePart == null)
            return 1;

        else
            return this.Size.CompareTo(comparePart.Size);
    }
    public bool Equals(Movies other)
    {
        if (other == null) return false;
        return (this.Size.Equals(other.Size));
    }
}

public class Images : IEquatable<Images>, IComparable<Images>
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public int Size { get; set; }
    public string Resolution { get; set; }
    public string FullSize { get; set; }
    public Images(string name, string Extension, string size, string resolution)
    {
        char[] delimiters = { 'K', 'B', 'M', 'G' };
        this.Name = name;
        this.Extension = Extension;
        this.FullSize = size;
        this.Size = Int32.Parse(size.TrimEnd(delimiters));
        this.Resolution = resolution;
    }
    public int CompareTo(Images comparePart)
    {
        if (comparePart == null)
            return 1;

        else
            return this.Size.CompareTo(comparePart.Size);
    }
    public bool Equals(Images other)
    {
        if (other == null) return false;
        return (this.Size.Equals(other.Size));
    }
}

namespace WindowsFormsApplication1
{

    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fileText = richTextBox1.Text;
            string[] filesString = fileText.Split('\n');
            List<TextFiles> textFiles = new List<TextFiles>();
            List<Movies> Movies = new List<Movies>();
            List<Images> Images = new List<Images>();
            //Пихаем по разным типам
            foreach (string file in filesString)
            {
                char[] delimiters = { '(', ')' };
                char[] trim_delim = { 'B', 'B', 'G', 'K' };
                string[] parseFile = file.Split(':');
                string[] paramsFile = parseFile[1].Split(';');
                string full_name = paramsFile[0];
                string[] name = full_name.Split(delimiters)[0].Split('.');
                string extension = name[name.Length - 1];
                switch (parseFile[0])
                {
                    case "Text":
                        {
                            TextFiles textFile = new TextFiles(full_name.Split(delimiters)[0], extension, full_name.Split(delimiters)[1],paramsFile[1]);
                            textFiles.Add(textFile);
                            break;
                        }
                    case "Movie":
                        {
                            Movies movie = new Movies(full_name.Split(delimiters)[0], extension, full_name.Split(delimiters)[1], paramsFile[1], paramsFile[1]);
                            Movies.Add(movie);
                            break;
                        }
                    case "Image":
                        {
                            Images image = new Images(full_name.Split(delimiters)[0], extension, paramsFile[0].Split(delimiters)[1], paramsFile[1]);
                            Images.Add(image);
                            break;
                        }
                }
            }
            //Сортируем по размеру
            textFiles.Sort();
            Movies.Sort();
            Images.Sort();
            //Выводим
            richTextBox2.Text = "Text files: \r\n";
            foreach(TextFiles file in textFiles)
            {
                richTextBox2.Text += "\t" + file.Name + "\r\n\t\t" + "Extension:" + file.Extension + "\r\n\t\t" + "Size:" + file.FullSize + "\r\n\t\t" + "Content:\"" + file.Content + "\"";
            }
            richTextBox2.Text += "\r\nMovies:\r\n";
            foreach (Movies file in Movies)
            {
                richTextBox2.Text += "\t" + file.Name + "\r\n\t\t" + "Extension:" + file.Extension + "\r\n\t\t" + "Size:" + file.FullSize + "\r\n\t\t" + "Resolution:" + file.Resolution + "\r\n\t\t" + "Lenght:" + file.Lenght;
            }
            richTextBox2.Text += "\r\nImages:\r\n";
            foreach (Images file in Images)
            {
                richTextBox2.Text += "\t" + file.Name + "\r\n\t\t" + "Extension:" + file.Extension + "\r\n\t\t" + "Size:" + file.FullSize + "\r\n\t\t" + "Resolution:" + file.Resolution + "\"";
            }
        }
    }
}

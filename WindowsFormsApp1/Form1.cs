using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string filepath_open_encode = string.Empty;
        public string input_string_encode = string.Empty;
        public string filepath_close_encode = string.Empty;
        public Bitmap new_bmp_encode = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filepath_open_encode = ofd.FileName;
                pictureBox1.Image = Image.FromFile(filepath_open_encode); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(filepath_open_encode);
            int x = bmp.Width;
            int y = bmp.Height;
            List<int> input = encode_input_string(input_string_encode);
            new_bmp_encode = encode_image(bmp, x, y, input);
            pictureBox2.Image = new_bmp_encode;
        }

        private List<int> encode_input_string(string input)
        {
            var input_list = new List<int>();

            foreach(char c in input)
            {
                int int_char = System.Convert.ToInt32(c);
                input_list.Add(int_char);
            }
            return input_list;
        }

        private Bitmap encode_image(Bitmap bmp_in, int x, int y, List<int> input)
        {
            Bitmap bmp_out = bmp_in;
            int i, j;
            for (i = 0; i < x; i++)
            {
                for (j = 0; j < y; j++)
                {
                    if (input.Count() == 0)
                    {
                        return bmp_out;
                    }
                    Color pixel = bmp_out.GetPixel(i, j);
                    
                    int pixel_argb = pixel.ToArgb();
                    int new_pixel_int = pixel_argb + input[0];

                    Color new_pixel = Color.FromArgb(new_pixel_int);

                    bmp_out.SetPixel(i, j, new_pixel);
                    input.RemoveAt(0);

                }
            }
            return bmp_out;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            input_string_encode = textBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_dialog = new SaveFileDialog();
            if (save_dialog.ShowDialog() == DialogResult.OK)
            {
                filepath_close_encode = save_dialog.FileName;
                new_bmp_encode.Save(filepath_close_encode);
            }
        }
    }
}

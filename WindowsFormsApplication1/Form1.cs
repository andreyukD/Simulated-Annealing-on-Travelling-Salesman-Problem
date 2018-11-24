using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        const int rozmiar = 8;
	    double ee = 2.718;
        double T = 100;
	    double t;
	    double tmin = 1;
	    double it = 0; //iteracja

    	double min;//9999999
        int[] randMin = new int[rozmiar];

        double[,] tab = new double[rozmiar,rozmiar];

        int[,] koord = new int[rozmiar,2] {
		    {300,0},
            {200, 100 },
            {400,100},
            {0,200},
            {600,200},
            {200,300},
            {400,300},
            {300,400}
	    };

        /*Point[] points = new Point[] { 
                new Point { X = 100, Y = 400 }, 
                new Point { X = 100, Y = 200 }, 
                new Point { X = 200, Y = 100 },
                new Point { X = 300, Y = 200 },
                new Point { X = 300, Y = 400 }
            };*/

        List<Point> pointList = new List<Point>();



        int[] random = new int[rozmiar];
        int[] randOld = new int[rozmiar];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Brush aBrush = (Brush)Brushes.Black;
            Graphics g = this.CreateGraphics();

            for (int i = 0; i < rozmiar; i++)
            {
                g.FillRectangle(aBrush, pointList[i].X, pointList[i].Y, 4, 4);
            }
            /**/
            Graphics gr = e.Graphics;
            Pen p = new Pen(Color.BlueViolet, 1);
            /**/
            Pen p1 = new Pen(Color.LightGray, 1);
            Point l1 = new Point(100, 0);
            Point l2 = new Point(100, 600);
            Point l3 = new Point(200, 0);
            Point l4 = new Point(200, 600);
            Point l5 = new Point(300, 0);
            Point l6 = new Point(300, 600);
            Point l7 = new Point(400, 0);
            Point l8 = new Point(400, 600);
            Point l9 = new Point(500, 0);
            Point l10 = new Point(500, 600);
            Point l11 = new Point(600, 0);
            Point l12 = new Point(600, 600);
            gr.DrawLine(p1, l1, l2);
            gr.DrawLine(p1, l3, l4);
            gr.DrawLine(p1, l5, l6);
            gr.DrawLine(p1, l7, l8);
            gr.DrawLine(p1, l9, l10);
            gr.DrawLine(p1, l11, l12);
            Point l13 = new Point(0, 100);
            Point l14 = new Point(600, 100);
            Point l15 = new Point(0, 200);
            Point l16 = new Point(600, 200);
            Point l17 = new Point(0, 300);
            Point l18 = new Point(600, 300);
            Point l19 = new Point(0, 400);
            Point l20 = new Point(600, 400);
            Point l21 = new Point(0, 500);
            Point l22 = new Point(600, 500);
            Point l23 = new Point(0, 600);
            Point l24 = new Point(600, 600);
            gr.DrawLine(p1, l13, l14);
            gr.DrawLine(p1, l15, l16);
            gr.DrawLine(p1, l17, l18);
            gr.DrawLine(p1, l19, l20);
            gr.DrawLine(p1, l21, l22);
            gr.DrawLine(p1, l23, l24);
            /**/

            for (int i = 0; i < rozmiar; i++)
            {
                if (i != rozmiar - 1)
                {
                    gr.DrawLine(p, pointList[randOld[i]], pointList[randOld[i + 1]]);
                }
                else
                {
                    gr.DrawLine(p, pointList[randOld[i]], pointList[randOld[0]]);
                }
            }
            gr.Dispose();
            /**/
            
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            int sc = 1;//skala
            for (int i = 0; i < rozmiar; i++)
            {
                pointList.Add(new Point(sc * koord[i, 0], sc * koord[i, 1]));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            myStopwatch.Start();

            string t2 = textBox2.Text;
            T = Convert.ToDouble(t2);

            string t3 = textBox3.Text;
            tmin = Convert.ToDouble(t3);


            double a = 0;
            if (radioButton6.Checked)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                string t1 = textBox1.Text;
                a = Convert.ToDouble(t1);

                if (a < 0 || a > 1)
                {
                    MessageBox.Show("przedział musi być od 0 do 1");
                    return;
                }
            }
            min = 999999;
            label6.Text = ""+T;

            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            {
                MessageBox.Show("Proszę wybrać metodę zamiany miast (SWAP, INVERT, REVERSE)");
                return;
            }

            if (!radioButton4.Checked && !radioButton5.Checked && !radioButton6.Checked)
            {
                MessageBox.Show("Proszę wybrać metodę chłodzenia (logarytmiczny, liniowy, geometryczny)");
                return;
            }

            it = 0;

            for (int i = 0; i < rozmiar; i++)
            {
                for (int j = 0; j < rozmiar; j++)
                {
                    tab[i, j] = Math.Sqrt
                        (
                        Math.Pow(koord[j, 0] - koord[i, 0], 2) + Math.Pow(koord[j, 1] - koord[i, 1], 2)
                        );
                }
            }

            /*string output1="";
            int iii = 0;
            foreach (var item in koord)
            {   
                output1 += item + " ";
                iii++;
                if (iii % 2 == 0)
                {
                    output1 += "\n";
                }
            }
            MessageBox.Show(output1);*/

            /*string output = "";
            int ii = 0;
            foreach (var item in tab)
            {
                output += item + " ";
                ii++;
                if (ii % rozmiar == 0)
                {
                    output += "\n";
                }
            }
            MessageBox.Show(output);*/

            for (int i = 0; i < rozmiar; i++)
            {//tworze tablice indeksow dla miast
                random[i] = i;
            }

            /*string output2 = "";
            int iii = 0;
            foreach (var item in random)
            {
                output2 += item + " ";
                iii++;
                    output2 += "\n";
            }
            MessageBox.Show(output2);*/

            Random rnd = new Random();
            rnd.Next();

            int jj = 0;
            for (int i = rozmiar - 1; i >= 0; i--)
            {

                if (i == 0)
                {
                    jj = rnd.Next(0, 1); ;
                }
                else
                {
                    jj = rnd.Next(0, i); ;
                }

                int temp = random[i];
                random[i] = random[jj];
                random[jj] = temp;
            }

            for (int i = 0; i < rozmiar; i++)
            {
                randOld[i] = random[i];
            }

           
            /*string o = "";
            for (int i = 0; i < rozmiar; i++)
            {
                o += random[i] + " ";
            }
            MessageBox.Show(o);

            string oo = "";
            for (int i = 0; i < rozmiar; i++)
            {
                oo += randOld[i] + " ";
            }
            MessageBox.Show(oo);

            /**/

            double sum = 0;
            for (int i = 0; i < rozmiar; i++)
            {
                if (i != rozmiar - 1)
                {
                    sum += tab[random[i], random[i + 1]];
                }
                else
                {
                    sum += tab[random[i], random[0]];
                }
            }

            label5.Text = String.Format("początkowy koszt: {0:F2}", sum);
            t = T;
          while (t > tmin)
            //for (int i1 = 0; i1 < 20;i1++ )
            {

                int rand1 = rnd.Next(rozmiar);
                int rand2 = rnd.Next(rozmiar);

                while (rand2 == rand1)
                {
                    rand2 = rnd.Next(rozmiar); ;
                }
                int ttmp;
                if (rand1 > rand2)
                {
                    ttmp = rand1;
                    rand1 = rand2;
                    rand2 = ttmp;
                }
              /**/
              /*  string ra1 = "";
                for (int i = 0; i < rozmiar; i++)
                {
                    ra1 += random[i] + " ";
                }
                
              /**/
                if (radioButton1.Checked)
                {
                    label3.Text = "zamiana typu SWAP";

                    int temp1 = random[rand1];
                    random[rand1] = random[rand2];
                    random[rand2] = temp1;
                }

                if (radioButton2.Checked)
                {
                    label3.Text = "zamiana typu INVERT";
                    int tempRev;
                    for (int rev = rand1, revJ = rand2; rev < revJ; rev++, revJ--)
                    {
                        tempRev = random[rev];
                        random[rev] = random[revJ];
                        random[revJ] = tempRev;
                    }
                }

                if (radioButton3.Checked)
                {
                    label3.Text = "zamiana typu INSERT";
                    int temp;
                    temp = random[rand1+1];//3
                    random[rand1+1] = random[rand1];
                    random[rand1] = temp;

                    int temp1;
                    temp1 = random[rand1];
                    random[rand1] = random[rand2];
                    random[rand2] = temp1;

                    int temp2;
                    for (int i = rand1+2; i < rand2; i++)
                    {
                        temp2=random[rand2];
                        random[rand2] = random[rand2 - 1];
                        random[rand2-1] = temp2;
                    }
                }

                string ra = "";
                for (int i = 0; i < rozmiar; i++)
                {
                    ra += random[i] + " ";
                }
                //MessageBox.Show("Metoda "+label3.Text +"\n stara droga" + ra1 +"\n nowa droga" + ra + "\n i j = " + rand1 + " " + rand2);
               /**/

                /*cout << "rand1 = " << rand1 << endl;
                cout << "rand2 = " << rand2 << endl;*/

                /* dwa elementa zamieniamy cout */

                /*cout << "zamieniamy dwa miasta w naszej drodze ";
                for (int i = 0; i < size; i++)
                {
                    cout << random[i] << " ";
                }
                cout << endl;*/

                /* dwa elementa zamieniamy cout*/

                /* koszt dla nowego rozwiazania */

                double sum1 = 0;
                for (int i = 0; i < rozmiar; i++)
                {
                    if (i != rozmiar - 1)
                    {
                        sum1 += tab[random[i], random[i + 1]];
                    }
                    else
                    {
                        sum1 += tab[random[i], random[0]];
                    }
                }

                //cout << "koszt przejazdu = " << sum1 << endl;

                /* koszt dla nowego rozwiazania */

                //cout << sum << " " << sum1 << endl;

                if (sum1 < sum)
                {
                    sum = sum1;
                    /*cout << sum << endl;*/

                    for (int i = 0; i < rozmiar; i++)
                    {
                        randOld[i] = random[i];
                    }
                }

                else
                {
                    double praw = 100 * Math.Pow(ee, (-(sum1 - sum) / t));
                    double rand3 = (rnd.Next(0, 100)) + 1;
                    /*cout << "p* = " << praw << endl;
                    cout << "rand3 = " << rand3 << endl;*/

                    if (praw > rand3)
                    {
                        sum = sum1;

                        for (int i = 0; i < rozmiar; i++)
                        {
                            randOld[i] = random[i];
                        }
                    }
                }//else

                if (sum < min)
                {
                    min = sum;

                    for (int i = 0; i < rozmiar; i++)
                    {
                        randMin[i] = randOld[i];
                    }
                    label12.Text = "na iteracji № " + it;
                }

                /**/

                label7.Text = String.Format("minimalny znaleziony : {0:F2}", min);
                

              
                /* */
                if (radioButton4.Checked)
                {
                    label4.Text = "liniowy: t=T*(1/k)";
                    t = T * (1/(it+1));
                }

                if (radioButton5.Checked)
                {
                    label4.Text = "logarytmiczny: t=T*(1/(1+log(k)))";
                    t = T * (1 / (1 + Math.Log(it+1)));
                }

                if (radioButton6.Checked)
                {
                    label4.Text = "geometryczny: t = T*(a^k), a = [0,1]);";
                    t = T * (Math.Pow(a, (it + 1)));
                }

                
                label6.Text = "" + t;

                it++;
                /*cout << "iteracji: " << it << endl << endl;*/
                label1.Text="iteracji: " + it;
                label2.Text = String.Format("koszt: {0:F2}", sum); // Show 3 Decimel Points
                //Thread.Sleep(1000);
                Refresh();

            } //while

          myStopwatch.Stop();
          TimeSpan ts = myStopwatch.Elapsed;
          string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                      ts.Hours, ts.Minutes, ts.Seconds,
                      ts.Milliseconds / 10);
          label11.Text = "" + elapsedTime;


            /* */
           // MessageBox.Show("" + sum);
          
         
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rozmiar; i++)
            {
                randOld[i] = randMin[i];
            }
        }

    }
}

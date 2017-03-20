using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
namespace GUI
{
    delegate void updateLabelTextDelegate(string newText);
   
    public partial class Form1 : Form
    {
        DateTime time;
        int mode = -1;
        int mode2 = -1;

        public Form1()
        {
            InitializeComponent();
            time = DateTime.Now;
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen != true)
                {
                    serialPort1.PortName = comboBox1.Text.ToString();
                    serialPort1.Open(); 
                    button1.Text = "Disconnect";
                    timer3.Enabled = true;
                }
                else
                {
                    serialPort1.Close();
                    button1.Text = "Connect";
                }
            }
            catch (Exception t)
            { MessageBox.Show(t.Message.ToString()); }
        }
        string Separator = "-";
        int counter = 0;
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {             
            try
            {
                counter++;
                //string data = serialPort1.ReadLine();

              string data=serialPort1.ReadExisting();
            // update();
                String[] dtParsing = Regex.Split(data, Separator);

                foreach (string strpacket in dtParsing)
                {
                    //update("P= "+strpacket+"\n");
                    string id = strpacket.Substring(0, 1);
                    //update("id= " + id.ToString() + "\n");
                    if (id == "N")
                    {
                        int light_val = Convert.ToInt32(strpacket.Substring(1, strpacket.Length - 1));
                       // update("light= " + light_val.ToString() + "\n");
                        update_Light(light_val.ToString());
                    }
                    else if (id == "K")
                    {
                        int Temp_val = Convert.ToInt32(strpacket.Substring(1, strpacket.Length - 1));
                        Temp_val = Convert.ToInt32((Temp_val /7.75));
                        //if (Temp_val < 30)
                        {
                            //Debug.Print("x= "+x.ToString()+"\n");
                            // double x1 = (Convert.ToDouble(Temp_val * 5) / 1023.0);
                            //Debug.Print("x1= " + x1.ToString() + "\n");
                            //  double Temp_val1 = (Convert.ToDouble(Temp_val) - 0.5) * 100;
                            update_temp(Temp_val.ToString());
                           // update_temp(Temp_val.ToString());
                            
                        }

                    }
                    else if (id == "J")
                    {
                        int Hum_val = Convert.ToInt32(strpacket.Substring(1, strpacket.Length - 1));

                        Hum_val = Convert.ToInt32((Hum_val * 0.003125 - 0.958) / 0.0307);
                        
                        update_Humidity(Hum_val.ToString());

                    }else if (id == "X")
                    {
                        int light_val = Convert.ToInt32(strpacket.Substring(1, strpacket.Length - 1));
                        // update("light= " + light_val.ToString() + "\n");
                        update_Light2(light_val.ToString());
                    }
                    else if (id == "Y")
                    {
                        int Temp_val = Convert.ToInt32(strpacket.Substring(1, strpacket.Length - 1));
                        Temp_val = Convert.ToInt32((Temp_val/7.75));
                        //  if (Temp_val < 1023)
                        {
                            //Debug.Print("x= "+x.ToString()+"\n");
                            // double x1 = (Convert.ToDouble(Temp_val * 5) / 1023.0);
                            //Debug.Print("x1= " + x1.ToString() + "\n");
                            //  double Temp_val1 = (Convert.ToDouble(Temp_val) - 0.5) * 100;
                            if (Temp_val > 17)
                            {
                                update_temp2(Temp_val.ToString());
                            }

                        }

                    }
                    else if (id == "Z")
                    {
                        int Hum_val = Convert.ToInt32(strpacket.Substring(1, strpacket.Length - 1));
                        Hum_val = Convert.ToInt32((Hum_val * 0.003125 - 0.958) / 0.0307);
                        update_Humidity2(Hum_val.ToString());

                    }
                }
            }
            catch (Exception f) { 
               // MessageBox.Show(f.Message.ToString());
            }
        }

       
        private void update_temp(string newText)
        {
            
           // Debug.Print("x11= " +x1.ToString() + "\n");
           // newText = Convert.ToInt32(x1.ToString()).ToString();
            if (textBox1.InvokeRequired)
            {
                updateLabelTextDelegate del = new updateLabelTextDelegate(update_temp);
               textBox1.Invoke(del, new object[] { newText });
            }
            else
            {
                textBox1.Text = newText;
            }

        }

        private void update_Light(string newText)
        {
            if (textBox2.InvokeRequired)
            {
                updateLabelTextDelegate del = new updateLabelTextDelegate(update_Light);
                textBox2.Invoke(del, new object[] { newText });
            }
            else
            {
                textBox2.Text = newText;
            }

        }

        private void update_Humidity(string newText)
        {
            if (textBox3.InvokeRequired)
            {
                updateLabelTextDelegate del = new updateLabelTextDelegate(update_Humidity);
                textBox3.Invoke(del, new object[] { newText });
            }
            else
            {
                textBox3.Text = newText;
            }

        }

        private void update_temp2(string newText)
        {
 
            if (textBox20.InvokeRequired)
            {
                updateLabelTextDelegate del = new updateLabelTextDelegate(update_temp2);
                textBox20.Invoke(del, new object[] { newText });
            }
            else
            {
                textBox20.Text = newText;
            }

        }

        private void update_Light2(string newText)
        {
            if (textBox21.InvokeRequired)
            {
                updateLabelTextDelegate del = new updateLabelTextDelegate(update_Light2);
                textBox21.Invoke(del, new object[] { newText });
            }
            else
            {
                textBox21.Text = newText;
            }

        }

        private void update_Humidity2(string newText)
        {
            if (textBox19.InvokeRequired)
            {
                updateLabelTextDelegate del = new updateLabelTextDelegate(update_Humidity2);
                textBox19.Invoke(del, new object[] { newText });
            }
            else
            {
                textBox19.Text = newText;
            }

        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("2");//50
            }
            mode = 2;
            timer2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("1");//49
            }
            mode = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
               // serialPort1.Write("0");//48
                serialPort1.Write("0");
            }
            mode = 0;
        }

        int flage = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox13.Text = DateTime.Now.Hour.ToString();
            textBox14.Text = DateTime.Now.Minute.ToString();
            textBox15.Text = DateTime.Now.Second.ToString();

            if (mode == 0)
            {
                if ((textBox7.Text == textBox13.Text) && (textBox9.Text == textBox14.Text) && (textBox8.Text == textBox15.Text))//ON
                {
                    flage = 4;
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("4");
                    }

                }
                if ((textBox13.Text == textBox10.Text) && (textBox14.Text == textBox12.Text) && (textBox15.Text == textBox11.Text))//OFF
                {
                    flage = 5;
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("5");
                    }
                }
            }
            if (mode == 71)
            {
                if ((textBox31.Text == textBox13.Text) && (textBox33.Text == textBox14.Text) && (textBox32.Text == textBox15.Text))//ON
                {
                    flage = 89;
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("Y");
                    }

                }
                if ((textBox13.Text == textBox28.Text) && (textBox14.Text == textBox30.Text) && (textBox15.Text == textBox29.Text))//OFF
                {
                    flage = 85;
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write("U");
                    }
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox2.Text) <= Convert.ToInt32(textBox22.Text))
                {
                    serialPort1.Write("6");
                    serialPort1.Write("A");//turn the artificial lights ON
                    textBox5.Text = "ON";

                }
                else
                {
                    serialPort1.Write("6");
                    serialPort1.Write("B");//Turn the artificial lights OFF
                    textBox5.Text = "OFF";
                }

                if (Convert.ToInt32(textBox1.Text) >= Convert.ToInt32(textBox23.Text))
                {
                    serialPort1.Write("7");
                    serialPort1.Write("C");//turn the coller ON
                    textBox6.Text = "ON";
                }
                else
                {
                    serialPort1.Write("7");
                    serialPort1.Write("D");//turn the cooler OFF
                    textBox6.Text = "OFF";
                }
                if (Convert.ToInt32(textBox3.Text) <= Convert.ToInt32(textBox24.Text))
                {
                    serialPort1.Write("8");
                    serialPort1.Write("E");//turn the irrigation ON
                    textBox4.Text = "ON";
                }
                else
                {
                    serialPort1.Write("8");
                    serialPort1.Write("F");//Turn the irrigation OFF
                    textBox4.Text = "OFF";
                }

                ///////////////////////////
                if (Convert.ToInt32(textBox21.Text) <= Convert.ToInt32(textBox26.Text))
                {
                    serialPort1.Write("R");
                    serialPort1.Write("a");//turn the artificial lights ON
                    textBox17.Text = "ON";

                }
                else
                {
                    serialPort1.Write("R");
                    serialPort1.Write("b");//Turn the artificial lights OFF
                    textBox17.Text = "OFF";
                }

                if (Convert.ToInt32(textBox20.Text) >= Convert.ToInt32(textBox27.Text))
                {
                    serialPort1.Write("V");
                    serialPort1.Write("c");//turn the coller ON
                    textBox16.Text = "ON";
                }
                else
                {
                    serialPort1.Write("V");
                    serialPort1.Write("d");//turn the cooler OFF
                    textBox16.Text = "OFF";
                }
                if (Convert.ToInt32(textBox19.Text) <= Convert.ToInt32(textBox25.Text))
                {
                    serialPort1.Write("M");
                    serialPort1.Write("O");//turn the irrigation ON
                    textBox18.Text = "ON";
                }
                else
                {
                    serialPort1.Write("M");
                    serialPort1.Write("Q");//Turn the irrigation OFF
                    textBox18.Text = "OFF";
                }
            }
            catch (Exception t) { }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                // serialPort1.Write("0");//48
                serialPort1.Write("G");
            }
            mode = 71;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("P");//49
            }
            mode = 80;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("I");//50
            }
            mode = 73;
            timer2.Enabled = true;
        }

        bool switch_ = false;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                switch_ = !switch_;
                if (switch_ == true)
                {
                    serialPort1.Write("s");
                }
                if (switch_ == false)
                {
                    serialPort1.Write("r");
                }
            }
            
        }
    }
}

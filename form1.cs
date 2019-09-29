using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlClass;
using MemClass;
using System.Diagnostics;   // อย่าลืม

namespace Flyff_Bot
{
    public partial class Form1 : Form
    {
        IntPtr mem;
        IntPtr GameHD;
        int pid;
        uint Neuz;
        int[] pt_HP = { 0x0, 0x708 }; 

        int MAXHP = 0;
        int PC_HP = 100;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            LoadPID();
            mem = BotMem.MemOpen(pid);
            //Neuz = BotMem.ModuleGetBase(mem, "Neuz.exe");
            GameHD = BotControl.WinGetHandle("3F FLYFF Free");
            timer1.Start();
        }

        private void LoadPID()
        {
            comboBox1.Items.Clear();
            Process[] MyProcess = Process.GetProcessesByName("Neuz"); //ใส่ชื่่อโปรเซสด้วยนะครับ
            for (int i = 0; i < MyProcess.Length; i++)
            {
                comboBox1.Items.Add(MyProcess[i].Id);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pid = (int)comboBox1.SelectedItem; // pid ที่เราจะเอาไปใช้งาน
            MessageBox.Show(comboBox1.SelectedItem.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int HP = BotMem.PointerRead4Byte(mem, (uint)pid + 0x6C4630, pt_HP);
            if (MAXHP <= HP)
            {
                MAXHP = HP;
                {
                    PC_HP = (HP * 100) / MAXHP;
                    progressBar1.Value = PC_HP;
                }
            }
        }
    }
}

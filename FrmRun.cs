using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Run
{
    public partial class FrmRun : Form
    {
        public FrmRun()
        {
            InitializeComponent();
        }

        Thread hiloHora;
        Thread hiloMinuto;
        Thread hiloSegundo;

        int iHora = 0;
        int iMinuto = 0;
        int iSegundo = 0;

        private void btnRun_Click(object sender, EventArgs e)
        {

            hiloSegundo = new Thread(new ThreadStart(this.Segundo));

            hiloSegundo.Start();
        }

        public void Hora()
        {

            iHora++;

            if (iHora > 12)
            {
                iHora = 0;
            }

            this.Invoke((MethodInvoker)delegate
            {
                if (iHora < 10)
                {
                    lblHora.Text = "0" + iHora.ToString();
                }
                else
                {
                    lblHora.Text = iHora.ToString();
                }
            });
        }

        public void Minuto()
        {
            iMinuto++;

            if (iMinuto == 60)
            {
                iMinuto = 0;
                hiloHora = new Thread(new ThreadStart(this.Hora));
                hiloHora.Start();
            }

            this.Invoke((MethodInvoker)delegate
            {
                if (iMinuto < 10)
                {
                    lblMinuto.Text = "0" + iMinuto.ToString();
                }
                else
                {
                    lblMinuto.Text = iMinuto.ToString();
                }
            });
        }

        public void Segundo()
        {
            while (true)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (iSegundo < 10)
                    {
                        lblSegundo.Text = "0" + iSegundo.ToString();
                    }
                    else
                    {
                        lblSegundo.Text = iSegundo.ToString();
                    }
                });

                Thread.Sleep(1000);

                iSegundo++;

                if (iSegundo == 60)
                {
                    iSegundo = 0;
                    hiloMinuto = new Thread(new ThreadStart(this.Minuto));
                    hiloMinuto.Start();
                }
            }
        }

        private void FrmRun_FormClosing(object sender, FormClosingEventArgs e)
        {
            hiloHora.Abort();
            hiloMinuto.Abort();
            hiloSegundo.Abort();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JewLogger
{
    public partial class frmSending : Form
    {
        public frmSending()
        {
            InitializeComponent();
        }

        private void btnSendToClient_Click(object sender, EventArgs e)
        {

            string clientPacket = AddCharacters(txtSendData.Text + (char)1);
            byte[] data = Encoding.Default.GetBytes(clientPacket);

            try
            {
                Form1.frmMain.TcpForwarder.DestinationState.SourceSocket.Send(data, data.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSendToServer_Click(object sender, EventArgs e)
        {
            string sendPacket = AddCharacters("@" + HabboEncoding.encodeB64(txtSendData.Text.Length) + txtSendData.Text);
            byte[] data = Encoding.Default.GetBytes(sendPacket);
            try
            {
                Form1.frmMain.TcpForwarder.DestinationState.DestinationSocket.Send(data, data.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    
        private string AddCharacters(string packet)
        {
            string newPacket = packet;

            for (int i = 0; i < 14; i++)
            {
                newPacket = newPacket.Replace("{" + i + "}", "" + (char)i);
            }

            return newPacket;
        }

        private void frmSending_Load(object sender, EventArgs e)
        {

        }
    }
}

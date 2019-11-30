using AgendaContatos.Entities;
using AgendaContatos.Utils;
using AgendaContatos.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AgendaContatos
{
    public partial class frmAgendaContatos : Form
    {
        private OperacaoEnum acao;

        public frmAgendaContatos()
        {
            InitializeComponent();
        }

        private void frmAgendaContatos_Shown(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesAdicionarAtualizarExcluir(true);
            AlterarEstadoCampos(false);
            CarregarListaContatos();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesAdicionarAtualizarExcluir(false);
            AlterarEstadoCampos(true);
            acao = OperacaoEnum.INCLUIR;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesAdicionarAtualizarExcluir(false);
            AlterarEstadoCampos(true);
            acao = OperacaoEnum.ALTERAR;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza?", "Pergunta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int indiceExcluido = lbxContatos.SelectedIndex;
                lbxContatos.SelectedIndex = 0;
                lbxContatos.Items.RemoveAt(indiceExcluido);
                List<Contato> contatoList = new List<Contato>();
                foreach (Contato contato in lbxContatos.Items)
                {
                    contatoList.Add(contato);
                }
                ManipuladorArquivos.EscreverArquivo(contatoList);
                CarregarListaContatos();
                LimparCampos();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Contato contato = new Contato
            {
                Nome = txtNome.Text,
                Email = txtEmail.Text,
                Telefone = mktTelefone.Text
            };
            List<Contato> contatosList = new List<Contato>();
            foreach (Contato c in lbxContatos.Items)
            {
                contatosList.Add(c);
            }

            if (acao == OperacaoEnum.INCLUIR)
            {
                contatosList.Add(contato);
            }
            else
            {
                int indice = lbxContatos.SelectedIndex;
                contatosList.RemoveAt(indice);
                contatosList.Insert(indice, contato);
            }

            ManipuladorArquivos.EscreverArquivo(contatosList);
            CarregarListaContatos();
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesAdicionarAtualizarExcluir(true);
            LimparCampos();
            AlterarEstadoCampos(false);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesAdicionarAtualizarExcluir(true);
            AlterarEstadoCampos(false);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AlterarEstadoCampos(bool estado)
        {
            txtNome.Enabled = estado;
            txtEmail.Enabled = estado;
            mktTelefone.Enabled = estado;
        }

        private void AlterarBotoesSalvarECancelar(bool estado)
        {
            btnSalvar.Enabled = estado;
            btnLimpar.Enabled = estado;
        }

        private void AlterarBotoesAdicionarAtualizarExcluir(bool estado)
        {
            btnAdicionar.Enabled = estado;
            btnAtualizar.Enabled = estado;
            btnExcluir.Enabled = estado;
        }

        private void CarregarListaContatos()
        {
            lbxContatos.Items.Clear();
            lbxContatos.Items.AddRange(ManipuladorArquivos.LerArquivo().ToArray());
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            mktTelefone.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToShortDateString();
            toolStripStatusLabel5.Text = DateTime.Now.ToLongTimeString();
        }

        private void pcbInformacao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Agenda de Contatos 1.0 \nDesenvolvido por: Paulo Alves", "Sobre");
        }

        private void lbxContatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contato contato = (Contato)lbxContatos.Items[lbxContatos.SelectedIndex];
            txtNome.Text = contato.Nome;
            txtEmail.Text = contato.Email;
            mktTelefone.Text = contato.Telefone;
        }
    }
}

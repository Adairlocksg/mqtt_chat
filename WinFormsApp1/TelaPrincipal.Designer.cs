namespace WinFormsApp1
{
    partial class TelaPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lb_conteudo = new ListBox();
            tb_mensagem = new TextBox();
            btn_enviar = new Button();
            SuspendLayout();
            // 
            // lb_conteudo
            // 
            lb_conteudo.FormattingEnabled = true;
            lb_conteudo.ItemHeight = 15;
            lb_conteudo.Location = new Point(13, 13);
            lb_conteudo.Name = "lb_conteudo";
            lb_conteudo.Size = new Size(223, 319);
            lb_conteudo.TabIndex = 0;
            // 
            // tb_mensagem
            // 
            tb_mensagem.Location = new Point(13, 339);
            tb_mensagem.Name = "tb_mensagem";
            tb_mensagem.Size = new Size(223, 23);
            tb_mensagem.TabIndex = 1;
            // 
            // btn_enviar
            // 
            btn_enviar.Location = new Point(12, 369);
            btn_enviar.Name = "btn_enviar";
            btn_enviar.Size = new Size(224, 23);
            btn_enviar.TabIndex = 2;
            btn_enviar.Text = "Enviar";
            btn_enviar.UseVisualStyleBackColor = true;
            btn_enviar.Click += btn_enviar_Click;
            // 
            // TelaPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(248, 403);
            Controls.Add(btn_enviar);
            Controls.Add(tb_mensagem);
            Controls.Add(lb_conteudo);
            Name = "TelaPrincipal";
            Text = "Chat MQTT";
            FormClosing += TelaPrincipal_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lb_conteudo;
        private TextBox tb_mensagem;
        private Button btn_enviar;
    }
}

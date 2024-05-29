using MQTTnet;
using MQTTnet.Client;

namespace WinFormsApp1
{
    public partial class TelaPrincipal : Form
    {

        /*O que está comentado é utilizando outra implementação (M2Mqtt) */
        //private MqttClient _mqttClient;
        private string _nomeAplicacao = "chat_1";
        private string _nomeAplicacaoResposta = "chat_2";
        private IMqttClient _mqttClient;
        public TelaPrincipal()
        {
            InitializeComponent();

            ConectarMqtt();
            //MqttClientConnectResult connAck = await _mqttClient
            //_mqttClient = new MqttClient("951727d767984add9c434ee6618d4130.s1.eu.hivemq.cloud", 8883, true, MqttSslProtocols.TLSv1_2, null, null);

            //ConectarMqtt();
        }

        public async void ConectarMqtt()
        {
            _mqttClient = new MqttFactory().CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithCredentials("hivemq.webclient.1716980644116", "f40!.HF5n3o&JqP%KuDe")
                .WithTlsOptions(new MqttClientTlsOptions
                {
                    UseTls = true
                })
                .WithTcpServer("951727d767984add9c434ee6618d4130.s1.eu.hivemq.cloud", 8883)
                .Build();

            MqttClientConnectResult connAck = await _mqttClient!.ConnectAsync(options);

            if (!_mqttClient.IsConnected)
                throw new Exception("Não foi possível conectar ao broker");

            _mqttClient.ApplicationMessageReceivedAsync += async (m) => await MensagensRecebidas(m);

            MqttClientSubscribeResult suback = await _mqttClient.SubscribeAsync($"chat/{_nomeAplicacao}", MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce);
        }

        private async Task MensagensRecebidas(MqttApplicationMessageReceivedEventArgs m)
        {
            var topico = m.ApplicationMessage.Topic;
            var mensagem = m.ApplicationMessage.ConvertPayloadToString();

            if (string.IsNullOrEmpty(topico) || string.IsNullOrEmpty(mensagem)) return;

            lb_conteudo.Invoke(new Action(() => { lb_conteudo.Items.Add($"**{mensagem}\r\n"); }));
        }


        private async void btn_enviar_Click(object sender, EventArgs e)
        {
            var mensagem = tb_mensagem.Text;

            if (string.IsNullOrEmpty(mensagem)) return;

            if (!_mqttClient.IsConnected) ConectarMqtt();

            try
            {
                lb_conteudo.Invoke(new Action(() => { lb_conteudo.Items.Add($"{mensagem}\r\n"); }));
                tb_mensagem.Clear();
                MqttClientPublishResult puback = await _mqttClient.PublishStringAsync($"chat/{_nomeAplicacaoResposta}", mensagem, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void TelaPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            await _mqttClient.DisconnectAsync();
        }

        //private void MensagensRecebidas(object sender, MqttMsgPublishEventArgs e)
        //{
        //    var topico = e.Topic;
        //    var mensagem = Encoding.UTF8.GetString(e.Message);

        //    if (string.IsNullOrEmpty(topico) || string.IsNullOrEmpty(mensagem)) return;

        //    lb_conteudo.Invoke(new Action(() => { lb_conteudo.Items.Add($"**{mensagem}\r\n"); }));
        //}

        //private void ConectarMqtt()
        //{
        //    if (_mqttClient is null || _mqttClient.IsConnected) return;

        //    _mqttClient.Connect(_nomeAplicacao, "hivemq.webclient.1716980644116", "f40!.HF5n3o&JqP%KuDe");

        //    if (!_mqttClient.IsConnected)
        //        throw new Exception("Não foi possível conectar ao broker");

        //    _mqttClient.MqttMsgPublishReceived += MensagensRecebidas;

        //    _mqttClient.Subscribe(new string[] { $"chat/{_nomeAplicacao}" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        //}

        //private void btn_enviar_Click(object sender, EventArgs e)
        //{


        //var mensagem = tb_mensagem.Text;

        //if (string.IsNullOrEmpty(mensagem)) return;

        //if (!_mqttClient.IsConnected) ConectarMqtt();


        //try
        //{
        //    lb_conteudo.Invoke(new Action(() => { lb_conteudo.Items.Add($"{mensagem}\r\n"); }));
        //    tb_mensagem.Clear();
        //    _mqttClient.Publish($"chat/{_nomeAplicacaoResposta}", Encoding.UTF8.GetBytes(mensagem), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //    throw;
        //}
        //}

        //public void TelaPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    await _mqttClient.Disconnect();
        //}
    }
}

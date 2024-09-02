import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import emailjs from '@emailjs/browser';

interface FAQ {
  question: string;
  answers: string[];
  showAnswer: boolean;
}

@Component({
  selector: 'app-sac',
  templateUrl: './sac.component.html',
  styleUrls: ['./sac.component.css'],
})
export class SacComponent {
  from_name: string = '';
  from_email: string = '';
  message: string = '';

  async send() {
    emailjs.init('ipEEfm5fDU1GgnoVa');
    let response = await emailjs.send('service_ngo4usw', 'template_ce2lfm3', {
      from_name: this.from_name,
      from_email: this.from_email,
      message: this.message,
    });

    alert('Mensagem Enviada com Sucesso');
    this.from_name = '';
    this.from_email = '';
    this.message = '';
  }
  

  onSubmit() {
    throw new Error('Method not implemented.');
  }


  faqs: FAQ[] = [
    {
      question: 'Como posso entrar em contato com o suporte?',
      answers: [
        'Você pode entrar em contato através do nosso WhatsApp, lá nós poderemos resolver seu problema com maior facilidade e agilidade.',
      ],
      showAnswer: false,
    },
    {
      question: 'Dúvidas sobre o seu cadastro?',
      answers: [
        '1. Como alterar meu e-mail?: Por questões de segurança, não é possível fazer a alteração de e-mail diretamente em nosso site. Entre em contato com nosso Atendimento, que irá lhe requisitar uma comprovação de identidade para fazer a alteração.',
        '2. Como atualizar meu cadastro?: Para atualizar seu cadastro, é necessário acessar o nosso site, na parte superior clique em "Entrar". Faça login (e-mail e senha), selecione a opção "Informações de contato", e nesta tela, em "Seus dados" faça as atualizações necessárias.',
        '3. Por que eu preciso me cadastrar na BlitzTech?: É através do seu cadastro com senha que podemos te garantir segurança no momento da sua compra. Além disso, é pelo cadastro que seus dados como nome, endereço e telefone ficam atualizados. Precisamos desses dados para poder entregar seus produtos corretamente. E não se preocupe, temos um sistema que protege totalmente suas informações pessoais.',
      ],
      showAnswer: false,
    },
    {
      question: 'Dúvidas sobre o pagamento?',
      answers: [
        'Formas de pagamento:',
        'Cartão de Crédito: Na BlitzTech você pode fazer suas compras com cartão de crédito das bandeiras: Visa, Mastercard, Hipercard, Elo, American Express e Paypal. O pagamento pode ser parcelado entre 1 até 12x.',
        'Boleto Bancário: O pagamento deverá ser feito a vista, e pode ser pago em qualquer banco.',
      ],
      showAnswer: false,
    },
    {
      question: 'Dúvida sobre entrega?',
      answers: [
        '1. Entrega atrasada: Primeiramente, faça seu login, vá em "Meus pedidos", escolha o pedido desejado e clique em "Ver". Lá você encontra o acompanhamento detalhado da sua compra, desde as Informações do seu pedido até rastreio. Confirme se o seu pedido foi aprovado ou verifique qualquer informação sobre as tentativas de entrega. Se o prazo já excedeu e não constar nenhuma informação, entre em contato com a nossa Central de Atendimento e a nossa equipe vai verificar o que ocorreu.',
        '2. Consta entregue e não recebi: Se você verificou no acompanhamento de "Meus Pedidos" que a entrega foi realizada, mas não recebeu o produto, fique tranquilo! Pode ser que a transportadora tenha feito uma baixa indevida de entrega ou entregue o produto para um terceiro responsável. Primeiramente, certifique-se na portaria ou confirme se algum responsável recebeu o produto.',
        '3. Pedido extraviado: Se você recebeu a informação de que seu pedido foi extraviado, entre em contato com a nossa Central de Atendimento para que a nossa equipe confirme essa informação com a transportadora.',
        '4. Quais dias e horários as entregas são realizadas?: As entregas são realizadas de segunda a sexta-feira das 8h às 18h, exceto feriados, podendo ocorrer variações de horários de acordo com a roteirização das filiais das transportadoras.',
      ],
      showAnswer: false,
    },
    {
      question: 'Qual o horário de atendimento?',
      answers: [
        'Nosso horário de atendimento é de segunda a sexta, das 9h às 18h.',
      ],
      showAnswer: false,
    },
  ];

  toggleAnswer(faq: FAQ): void {
    faq.showAnswer = !faq.showAnswer;
  }
}

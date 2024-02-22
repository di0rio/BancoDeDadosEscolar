// Obter o mês e o ano atuais
var data = new Date();
var mesAtual = data.getMonth() + 1;
var anoAtual = data.getFullYear();

// Selecionar o mês e o ano atuais ao carregar a página
document.getElementById('month').value = mesAtual;
document.getElementById('year').value = anoAtual;
var reservas = [];


// Chamar a função preencherCalendario ao carregar a página
preencherCalendario(mesAtual, anoAtual);



// Função para pegar os dados da tabela e preencher o vetor reservas
function pegarDadosTabela() {
    var tabela = document.getElementById('TabelaReservas');
    var linhas = tabela.getElementsByTagName('tr');
    //reservas = []; // Limpar o vetor reservas

    for (var i = 0; i < linhas.length; i++) { // Começar do 1 para ignorar o cabeçalho da tabela
        var colunas = linhas[i].getElementsByTagName('td');
        var reserva = {
            dataInicio: colunas[0].textContent,
            dataFim: colunas[1].textContent,
            horaInicio: colunas[2].textContent,
            horaFim: colunas[3].textContent,
            usuario: colunas[4].textContent,
            cor: colunas[5].textContent

        };
        reservas.push(reserva);
    }
    //Escutar o clique na data
    criarEvento();
    criarEventoReserva(reservas);
}


// Função para preencher o calendário
function preencherCalendario(mes, ano) {
    // Obter o elemento do calendário
    var calendario = document.getElementById('calendar-days');
    // Limpar o calendário
    calendario.innerHTML = '';
    // Obter o primeiro dia do mês
    var primeiroDia = new Date(ano, mes, 1).getDay();
    // Obter o número de dias no mês
    var numeroDias = new Date(ano, mes, 0).getDate();

    // Variável para acompanhar o dia atual
    var diaAtual = 1;



    // Loop para cada semana
    for (var i = 0; i < 6; i++) {
        // Criar uma nova linha
        var linha = document.createElement('tr');
        // Variável para verificar se a linha está vazia
        var linhaVazia = true;

        // Loop para cada dia da semana
        for (var j = 0; j < 7; j++) {

            // Se estamos na primeira semana e o dia da semana é menor que o primeiro dia do mês, ou se o dia atual é maior que o número de dias no mês, adicionar uma célula vazia
            if ((i === 0 && j < primeiroDia) || diaAtual > numeroDias) {
                var celula = document.createElement('td');
                celula.classList.add('text-center');
                celula.classList.add("Vazio");
                linha.appendChild(celula);
            } else {
                // Caso contrário, adicionar uma célula com o dia atual
                var celula = document.createElement('td');
                celula.textContent = diaAtual;
                linha.appendChild(celula);
                celula.classList.add('text-center');
                celula.classList.add("DataAgenda");
                diaAtual++;
                linhaVazia = false;
            }

        }
        if (!linhaVazia) {
            // Adicionar a linha ao calendário
            calendario.appendChild(linha);
        }
    }
    pegarDadosTabela()
}

document.getElementById('year').addEventListener('change', function () {
    var mesSelecionado = document.getElementById('month').value;
    var anoSelecionado = this.value;
    preencherCalendario(mesSelecionado, anoSelecionado);
    pegarDadosTabela();

});

// Supondo que os elementos select para o mês e o ano tenham os ids 'selectMes' e 'selectAno' respectivamente
document.getElementById('month').addEventListener('change', function () {
    var anoSelecionado = document.getElementById('year').value;
    var mesSelecionado = this.value;
    preencherCalendario(mesSelecionado, anoSelecionado);
    pegarDadosTabela();

});

// Função para criar um evento no clique da data
function criarEvento() {
    var celulas = document.getElementsByTagName('td');
    for (var i = 0; i < celulas.length; i++) {
        celulas[i].addEventListener('click', function () {
            if (this.classList.contains('DataAgenda')) {
                var dia = this.textContent;
                var mes = document.getElementById('month').value;
                var ano = document.getElementById('year').value;
                var data = dia + '/' + mes + '/' + ano;
                alert('Data selecionada: ' + data);
            }
        });
    }
}


// Função para criar eventos de reserva
function criarEventoReserva(reservas) {
    var celulas = document.getElementsByTagName('td');
    for (var i = 0; i < celulas.length; i++) {
        for (var j = 0; j < reservas.length; j++) {
            var dataInicio = new Date(reservas[j].dataInicio);
            var dataFim = new Date(reservas[j].dataFim);
            var diaCelula = parseInt(celulas[i].textContent);
            var mes = document.getElementById('month').value;
            var ano = document.getElementById('year').value;
            var dataCelula = new Date(ano, mes - 1, diaCelula);
            if (dataCelula >= dataInicio && dataCelula <= dataFim) {
                celulas[i].style.backgroundColor = reservas[j].cor;
                celulas[i].title = 'Reservado por ' + reservas[j].usuario + ' de ' + reservas[j].dataInicio + ' até ' + reservas[j].dataFim;
            }
        }
    }
}
// chamar essa função no carregamento da página

// Chamar a função pegarDadosTabela ao carregar a página
pegarDadosTabela();


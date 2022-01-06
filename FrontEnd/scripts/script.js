var tbody = document.querySelector('table tbody');
var aluno = {};
function Cadastrar() {
    aluno.nome = document.querySelector('#nome').value;
    aluno.sobrenome = document.querySelector('#sobrenome').value;
    aluno.telefone = document.querySelector('#telefone').value;
    aluno.ra = document.querySelector('#ra').value;

    if (aluno.id === undefined || aluno.id === 0) {

        salvarEstudantes('POST', 0, aluno);
    }
    else {
        salvarEstudantes('PUT', aluno.id, aluno);
    }
    limparCampos();
    Cancelar();
    carregaEstudantes();
    $('#exampleModal').modal('hide');
}

function deletarEstudante(id) {
    var xhr = new XMLHttpRequest();

    xhr.open('DELETE', `https://localhost:44346/api/Aluno/${id}`, false);

    xhr.send();
}

function excluir(estudante) {

    bootbox.confirm({
        title: "Excluir Aluno",
        message: `Tem certeza que deseja excluir o estudante ${estudante.nome} ${estudante.sobrenome}  ?`,
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Não'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Sim'
            }
        },
        callback: function (result) {
            if (result) {
                deletarEstudante(estudante.id);
                carregaEstudantes();
            }
        }
    });

}

function limparCampos() {
    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';
}

function novoAluno() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#titulo');

    btnSalvar.textContent = 'Cadastrar';

    aluno = {};

    exampleModalLabel.textContent = 'Cadastrar Aluno';

    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';

    $('#exampleModal').modal('show');
}

function Cancelar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#titulo');

    btnSalvar.textContent = 'Cadastrar';

    aluno = {};

    titulo.textContent = 'Cadastrar Aluno';

    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';

    $('#exampleModal').modal('hide');
}

function carregaEstudantes() {
    tbody.innerHTML = '';

    var xhr = new XMLHttpRequest();

    xhr.open('GET', `https://localhost:44346/api/Aluno/RetornaAlunos`, true);
    xhr.setRequestHeader("Authorization", sessionStorage.getItem('token'));

    xhr.onerror = function () {
        console.log('ERRO', XHR.readyState);
    }

    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var estudantes = JSON.parse(this.responseText);
                for (var indice in estudantes) {
                    adicionaLinha(estudantes[indice]);
                }
            }
            else if(this.status == 500){
                var erro = JSON.parse(this.responseText);
                console.log(erro.Message);
                console.log(erro.ExceptionMessage);
            }
        }
    }
    xhr.send();
}

function salvarEstudantes(metodo, id, corpo) {

    var xhr = new XMLHttpRequest();
    if (id === undefined || id === 0)
        id = '';

    xhr.open(metodo, `https://localhost:44346/api/Aluno/${id}`, false);

    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(corpo));

}
carregaEstudantes();

function editarEstudante(estudante) {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#titulo');

    btnSalvar.textContent = 'Salvar';
    exampleModalLabel.textContent = `Editando Aluno ${estudante.nome} ${estudante.sobrenome}`;

    document.getElementById('nome').value = estudante.nome;
    document.getElementById('sobrenome').value = estudante.sobrenome;
    document.getElementById('telefone').value = estudante.telefone;
    document.getElementById('ra').value = estudante.ra;

    aluno = estudante;
}

function adicionaLinha(estudante) {
    var trow = `
                        <tr>
                            <td>${estudante.nome}</td>
                            <td>${estudante.sobrenome}</td>
                            <td>${estudante.telefone}</td>
                            <td>${estudante.ra}</td>
                            <td> <button class="btn btn-info" data-bs-toggle="modal" data-bs-target="#exampleModal" onclick='editarEstudante(${JSON.stringify(estudante)})'>Editar</button>
                            <button class="btn btn-danger" onclick='excluir(${JSON.stringify(estudante)})'>Deletar</button> </td>
                       </tr>
                       `
    tbody.innerHTML += trow;
}
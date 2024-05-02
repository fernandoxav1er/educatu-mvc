

$('.close-alert').click(function () {
    $('.alert').hide('hide');
});

document.getElementById('perfil').addEventListener('change', function () {
    var perfilSelecionado = this.value;
    var comboPlano = document.getElementById('idPlano');

    if (perfilSelecionado === 'Aluno') {
        comboPlano.style.display = 'block';
    } else {
        comboPlano.style.display = 'none';
    }
});
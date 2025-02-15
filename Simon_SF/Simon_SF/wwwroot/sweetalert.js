Swal.fire({
    title: '¿Estás seguro?',
    text: 'Este cambio no se puede deshacer.',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Sí, eliminar',
    cancelButtonText: 'Cancelar'
}).then(result => {
    console.log(result.isConfirmed); // Verifica el valor de isConfirmed
});

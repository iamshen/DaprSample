
NProgress.configure({
    parent: '.main',
    showSpinner: false
});

function startProgress() {
    NProgress.inc();
}

function stopProgress() {
    NProgress.done();
}
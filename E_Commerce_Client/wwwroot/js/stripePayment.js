redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51Os0PQFoqZu0kGAIpMkkyhRyNdVn21vGhD4oGRmBaC3V2H8ZSnPJ8Zmiledhk9ndiBSMgVt6HAycs1JXKbGtzhan009twVFQ1n");
    stripe.redirectToCheckout({ sessionId: sessionId });
}
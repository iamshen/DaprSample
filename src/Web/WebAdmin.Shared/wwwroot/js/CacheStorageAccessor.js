async function openCacheStorage() {
    try {
        return await window.caches.open("WebAdmin");
    } catch (err) {
        return undefined;
    }
}

function createRequest(url, method, body = "") {
    const requestInit = {
        method: method
    };

    if (body != "") {
        requestInit.body = body;
    }

    const request = new Request(url, requestInit);

    return request;
}

export async function put(url, method, body = "", responseString) {
    const CACHING_DURATION = 7 * 24 * 3600;

    const expires = new Date();
    expires.setSeconds(expires.getSeconds() + CACHING_DURATION);

    const cachedResponseFields = {
        headers: { 'fluent-cache-expires': expires.toUTCString() },
    };

    const cache = await openCacheStorage();
    if (cache != null) {

        const request = createRequest(url, method, body);
        const response = new Response(responseString, cachedResponseFields);

        await cache.put(request, response);
    }
}

export async function get(url, method, body = "") {
    const cache = await openCacheStorage();
    if (cache == null) {
        return "";
    }

    const request = createRequest(url, method, body);
    const response = await cache.match(request);

    if (response == null) {
        return "";
    } else {
        const expirationDate = Date.parse(response.headers.get("cache-expires"));
        const now = new Date();
        // Check it is not already expired and return from the cache
        if (expirationDate > now) {
            const result = await response.text();

            return result;
        }
    }

    return "";
}

export async function remove(url, method, body = "") {
    const cache = await openCacheStorage();

    if (cache != null) {
        const request = createRequest(url, method, body);
        await cache.delete(request);
    }
}

export async function removeAll() {
    const cache = await openCacheStorage();

    if (cache != null) {
        cache.keys().then(function(names) {
            for (let name of names)
                cache.delete(name);
        });
        //let requests = await cache.keys();

        //for (let request in requests) {
        //    await cache.delete(request);
        //}
    }
}
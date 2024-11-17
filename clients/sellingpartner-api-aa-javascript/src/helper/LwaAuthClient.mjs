import superagent from 'superagent';

export class LwaAuthClient {
    #clientId = null;
    #clientSecret = null;
    #refreshToken = null;
    #accessToken = null;
    #accessTokenExpiry = null;
    /**
    * Constructs a new LwaAuthClient. 
    * @class
    * @param {String} clientId LWA client ID. Get this value from SP-API Developer Portal.
    * @param {String} clientSecret LWA client secret. Get this value from SP-API Developer Portal.
    * @param {String} refreshToken LWA refresh token. Get this value from SP-API Developer Portal.
    */
    constructor(clientId, clientSecret, refreshToken) {
        if (!clientId || typeof clientId !== 'string') {
            throw new Error(`invalid clientId.`);
        }
        if (!clientSecret || typeof clientSecret !== 'string') {
            throw new Error(`invalid clientSecret`);
        }
        if (!refreshToken || typeof refreshToken !== 'string') {
            throw new Error(`invalid refreshToken`);
        }
        this.#clientId = clientId;
        this.#clientSecret = clientSecret;
        this.#refreshToken = refreshToken;
    }

    /**
    * Either retrieve LWA access token or return access token if it already has valid token.
    * @returns {Promise<String>} LWA access token.
    */
    async getAccessToken() {
        if (this.#accessToken && this.#accessToken && this.#accessTokenExpiry > Date.now()) {
            console.log(`LWA access token already exists and is valid. ${this.#accessTokenExpiry}`);
            return Promise.resolve(this.#accessToken);
        }
        const token = await this.#doRefresh();
        if (!token || !token.access_token) {
            throw new Error(`Failed to refresh LWA token.`);
        }
        this.#accessToken = token.access_token;
        this.#accessTokenExpiry = new Date().getTime() + (token.expires_in * 1000);
        return this.#accessToken;
    }

    /**
     * Private method to execute LWA token refresh flow.
     * @returns {Promise<Object>} LWA token response.
     */
    #doRefresh = async () => {
        const res = await superagent.post('https://api.amazon.com/auth/o2/token')
            .send(`grant_type=refresh_token&refresh_token=${this.#refreshToken}&client_id=${this.#clientId}&client_secret=${this.#clientSecret}`)
            .set("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
        return res.body;
    }
}

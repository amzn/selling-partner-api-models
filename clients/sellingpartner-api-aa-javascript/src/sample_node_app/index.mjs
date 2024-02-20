import { AppConfig } from '../app.config.mjs';
import { LwaAuthClient } from '../helper/LwaAuthClient.mjs';
import { SellersApi, ApiClient as SellersApiClient } from '../../sdk/src/sellers/index.js';

(async () => {
    const lwaClient = new LwaAuthClient(AppConfig.lwaClientId, AppConfig.lwaClientSecret, AppConfig.lwaRefreshToken);
    const sellerApiClient = new SellersApiClient(AppConfig.endpoint);

    const sellerApi = new SellersApi(sellerApiClient);
    sellerApiClient.applyXAmzAccessTokenToRequest(await lwaClient.getAccessToken());
    const participations = await sellerApi.getMarketplaceParticipations();
    console.log(JSON.stringify(participations) + "\n**********************************");
})();

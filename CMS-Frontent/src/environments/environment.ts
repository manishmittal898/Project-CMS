// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  AESKey: "0123456789abcdef0123456789abcdef",
  //apiEndPoint: "http://localhost:31958/api/",
  apiEndPoint: "https://api.storeone.co.in/api/",
  sitePath: 'https://demo.storeone.co.in/',
  IsAutoLogin: false,
  GoogleClientId: "585225620513-vcqtov90e3d8eq47on9e8o8smkagifdp.apps.googleusercontent.com",
  GoogleClientSecret: "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"
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.

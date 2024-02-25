// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyDGxBN6F0v7o68gcOXCyCmgOkQP293yZj0",
  authDomain: "tryapp-6176e.firebaseapp.com",
  databaseURL: "https://tryapp-6176e-default-rtdb.firebaseio.com",
  projectId: "tryapp-6176e",
  storageBucket: "tryapp-6176e.appspot.com",
  messagingSenderId: "8839635440",
  appId: "1:8839635440:web:3fbf045edf66aa6b607d35",
  measurementId: "G-MRNY3XCE1Z"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);
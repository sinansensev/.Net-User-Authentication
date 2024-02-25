import { initializeApp } from "firebase/app";
import firebase from 'firebase/compat/app';

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

const app = initializeApp(firebaseConfig);

if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
}

export default app;

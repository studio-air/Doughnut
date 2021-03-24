const app = require('express')();
const http = require('http').createServer(app);
const io = require('socket.io')(http);

var connection;

mergeInto(LibraryManager.library, {

    Hello: function () {
        window.alert("Hello, world!");
        connection = io.connect();
    },
  
});
<?php

// Создаем подключение к базе данных MySQL с использованием PDO
$pdo = new PDO('mysql:host=localhost;dbname=oktybrskmerchshop', 'merchadmin', 'qwerty123');

// Проверяем, удалось ли подключиться к базе данных
if (!$pdo) {
    echo "Мы не подключились"; // Выводим сообщение об ошибке, если подключение не удалось
    exit(); // Завершаем выполнение скрипта
}

// Получаем данные из POST-запроса
$ID = $_POST['IDFromUnity']; // Получаем ID пользователя из запросов
$Money = $_POST['MoneyFromMoney']; // Получаем сумму денег из запросов

// Подготавливаем SQL-запрос для обновления баланса пользователя
$query = $pdo->prepare("UPDATE `Users` SET `money` = '$Money' WHERE `Users`.`id` = '$ID'");

// Выполняем подготовленный запрос
$query->execute();

?>
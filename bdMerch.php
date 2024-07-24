<?php
// Создаем соединение с базой данных MySQL с помощью PDO
  $pdo = new PDO('mysql:host=localhost;dbname=oktybrskmerchshop', 'merchadmin', 'qwerty123');
// Проверяем, успешно ли выполнено подключение к базе данных
if(!$pdo){
    echo "Мы не подключились"; // Если подключение не удалось, выводим сообщение
    exit(); // Завершаем выполнение скрипта
}

	
// Получаем данные из POST-запроса
$login = $_POST['login']; // Логин, введенный пользователем
$pass = $_POST['pass']; // Пароль, введенный пользователем
	
// Выполняем SQL-запрос для поиска пользователя с введенными логином и паролем
$query = $pdo->query("SELECT * FROM `Users` WHERE `login`='$login' AND `password`='$pass'");
// Извлекаем результат запроса в виде объекта
	$row = $query->fetch(PDO::FETCH_OBJ);
// Проверяем, найден ли пользователь
	if($row!="")
	{
		 // Если пользователь найден, выводим его данные
	    echo "ID".$row->id."|Login:".$row->login."|Password:".$row->password."|Money:".$row->money."|Email:".$row->email.";";
	}
	else
	{
		  // Если данных не существует, выводим сообщение об ошибке
	    echo "неверный логин или пароль";
	}
		
?>
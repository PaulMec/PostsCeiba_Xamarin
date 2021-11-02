# PostsCeiba_Xamarin
Prueba técnica de publicaciones y escritores en Xamarin Andriod

Capa de aplicación móvil:
PostsCeiba (aplicación a correr)

Capa de servicios:
Services/ServicesCeiba

Consumo de servicios:
Services/ServicesCeiba/PublicationsManagement.CS
Helper (GET, POST, PUT): Services/ServicesCeiba/HttpRequestHelper.CS

La prueba consta de 4 vistas:

Vista principal: la vista se compone de una imagen de la compañia, 2 botones (Escritores y Publicaciones), al hacer click en cada botón puedo visualizar cada consulta.

Vista Escritores: se consulta la lista completa de escritores con los detalles solicitados:
- Buscador
- Nombre escritor
- Telefono
- Email
- Botón mas detalles, En un popup donde se pueden detallarse datos extras del escritor o autor y sus respectivas publicaciones:
  + Nombre escritor
  + Telefono
  + Email
  + Ciudad
  + WebSite
  
  Lista de publicaciones:
  + Titulo de la publicación
  + Cuerpo de la publicación 


Vista Publicaciones: se consulta la lista completa de publicaciones sin relacionar autores con los detalles solicitados:
- Buscador
- Titulo de la publicaciones
- Cuerpo de la publicación 

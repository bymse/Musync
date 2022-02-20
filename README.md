## Idea

1. Read new posts from predefined list of vk communities
2. Find album info 
3. Add the album to spotify account

## Features

1. Web interface to add sources 
2. Show added albums in special feed + link to original post (iframe?)
3. Show sync errors in web interface + ability to drop the error.

## Impl details 

1. Separate "reader" and "synchronizator". Connect them through message queue (RabbitMQ)
2. Read post from the source, put post to the queue

### Reader impl 

1. Background service polls vk api. Takes posts that come after "latest post id" + 1h since the publication.
2. Parse post:
   1. If has album as attachment -> get album's author + title
   2. If has audio attached -> get first line of the post if it matches the regex [.*][-—][.*]. How to separate author from the title?  
3. If found album info -> put to the "albums to sync" queue
4. If no album info -> put to the "failed posts" queue

### Sync impl

1. Background service reads the "albums to sync queue" 
2. Find album in spotify. https://developer.spotify.com/documentation/web-api/reference/#/operations/search
3. Add album to the user's account (if it is allowed)
4. Put message to the "synced albums" queue if album was found
5. If was not found -> put to the "failed posts" queue

### Saver impl 

1. Background service reads the "synced albums" queue
2. Save info to the durable storage
3. Background service reads the "failed posts" queue
4. Save info to the durable storage
5. Store posts in separate table + UserPostLink for user's feed

### Database 

Users:
UserId, Spotify token, allow add to the account 

Pages:
id, userid, PageId (from vk), latest post id, last poll time

Posts feed:
id, userid, pageid, vk post id, WasSynced, Spotify album id

### Web interface

1. Add page
2. Setup sync: add token, checkbox for allow add
3. List of proceeded posts:
   1. Vk post link/widget
   2. Delete post from the list
   3. Link to spotify playlist
   4. Error text if not playlist available

## Links

1. Получение списка записей: https://vknet.github.io/vk/wall/get/
2. 
3.  
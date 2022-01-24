## Idea

1. Read new posts from predefined list of vk communities
2. Find album info 
3. Add the album to spotify account

## Features

1. Web interface to add sources 
2. Show added albums in special feed + link to original post (iframe?)
3. Show sync errors in web interface + ability to drop the error.

## Impl details 

1. Separate "reader" and "synchronizator". Connect them through message queue
2. Read post from the source, put post to the queue
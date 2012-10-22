PushoverCli
===========

a command line interface to post messages to Pushover.net, a messaging service

how it works
------------

lets be honest, this is a ridiculously simple application.  to get it to work you'll need to do a couple of things.

1. find the PushoverCli.App.exe.config.  if you don't see this file, chances are you don't have known file extensions visible.  google how to do that and then come back.
2. edit the configuration values.  you need to put in your user token and a default application token (don't worry, you can override these).
3. there is another config file called NLog.config.  if you want to change the way the logging works, thats the place to do it.

thats about it.  now you're ready to send messages

sending messages
----------------

any and all available api arguments are available to you.  the downside is that you must name them accurately in the commandline.
for instance a call for the default user from the default application with a message and priority would look like:
    PushoverCli.exe message="this is pretty awesome" priority=-1
have a look at the api for available parameters.  as stated earlier, you can override user and application by passing in the `user` or `token` parameters.  only if they are not supplied is the configuration used.

the log
---------------
extremely basic, but will give you status codes when the message fails to send, and will also give you the reason (field:reason).
as stated, you are welcome to customize the log if you'd like. just google NLog and figure it out from there.
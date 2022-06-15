var LibraryPageTool = {
    GoFullscreen: function()
    {
        var viewFullScreen = document.getElementById('#canvas');
 
        var ActivateFullscreen = function()
        {
            if (viewFullScreen.requestFullscreen) /* API spec */
            {  
                viewFullScreen.requestFullscreen();
            }
            else if (viewFullScreen.mozRequestFullScreen) /* Firefox */
            {
                viewFullScreen.mozRequestFullScreen();
            }
            else if (viewFullScreen.webkitRequestFullscreen) /* Chrome, Safari and Opera */
            {  
                viewFullScreen.webkitRequestFullscreen();
            }
            else if (viewFullScreen.msRequestFullscreen) /* IE/Edge */
            {  
                viewFullScreen.msRequestFullscreen();
            }
 
            viewFullScreen.removeEventListener('keyup', ActivateFullscreen);
        }
 
        viewFullScreen.addEventListener('keyup', ActivateFullscreen, false);
    }
};
mergeInto(LibraryManager.library, LibraryPageTool);
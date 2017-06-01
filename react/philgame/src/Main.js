import React from 'react';
import Flag from './Flag';

const styles = {
    playButton: {
        width: '100%',
        marginTop: 20,
        height:'50px'
    }
}

const Main = () => {
    return (
        <div>
            <h1>Het Filosofisch</h1>
            <Flag></Flag>
            <button style={styles.playButton}>Play!</button>
        </div>
    );
}

export default Main;
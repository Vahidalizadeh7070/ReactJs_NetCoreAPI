import react, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import moment from 'moment';
import NewsDetails from '../Page/NewsDetails';


export default function FutureGameList() {
    const [FutureListAPI, setFutureListAPI] = useState([]);
    const [Loading, setLoading] = useState(true)
    useEffect(() => {
        axios.get("https://localhost:44352/api/futurereleasegame").then((response) => {
            setLoading(true);
            setFutureListAPI(response.data);
            setLoading(false);
        }).catch((err) => err.name);
    }, [])

    return (
        <div>
            {
                Loading === true ?
                    <div className="row align-content-center">
                        <div className="d-flex justify-content-center p-5">
                            <div className="spinner-border text-info spinner-size" role="status">
                                <span className="visually-hidden">Loading...</span>
                            </div>
                        </div>
                        <div className="d-flex justify-content-center p-5">
                            Please wait
                        </div>
                    </div> :
                    FutureListAPI.map((data) => {
                        return (
                            <div key={data.id} className="">
                                <div className="list-group">
                                    <div href="#" className="list-group-item list-group-item-action pb-3 mb-3" aria-current="true">
                                        <div className="d-flex w-100 justify-content-between ">
                                            <h5 className="mb-1">{data.name}</h5>
                                            <p><i className="bi bi-clock-history text-success"></i> {moment(data.releaseDate).format('dddd, MMMM yyyy')}</p>
                                        </div>
                                        <small>Platforms: {data.platforms}</small>
                                    </div>
                                </div>
                            </div>

                        )
                    })
            }

        </div>
    )
}




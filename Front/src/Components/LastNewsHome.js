import react, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { BrowserRouter, Route,Switch } from 'react-router-dom';
import moment from 'moment';
import NewsDetails from '../Page/NewsDetails';


export default function LastNewsHome(props) {
    const BaseImageUrl = "https://localhost:44352/News/";
    const [NewsApi, setNewsApi] = useState([]);
    const [Loading, setLoading] = useState(true)
    useEffect(() => {
        if (props.categoryId === undefined) {
            axios.get("https://localhost:44352/api/News/").then((response) => {
                setLoading(true);
                setNewsApi(response.data);
                setLoading(false);
            }).catch((err) => err.name);
        }
        else {
            axios.get("https://localhost:44352/api/news/GetNewsByCategoryId/" + props.categoryId).then((response) => {
                setLoading(true);
                setNewsApi(response.data);
                setLoading(false);

            }).catch((err) => err.name);
        }
    }, [props.categoryId])

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
                    NewsApi.map((data) => {
                        return (
                            <div key={data.id}>
                                <div className="mb-4">
                                    <div className="">
                                        <div className="card">
                                            <div className="card-body">
                                                <div className="row">
                                                    <div className="col-md-4">
                                                        <img src={BaseImageUrl + data.imageOne} className="card-img-left img-fluid shadow" />
                                                    </div>
                                                    <div className="col-md-8">
                                                        <h2 className="fw-bold text-secondary">{data.title}</h2>
                                                        <hr />
                                                        <p className="text-secondary fw-light lh-lg">{data.about}</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="pt-2 card-footer">
                                                <div className="row">
                                                    <div className="col-md-3 col-sm-3 col-xs-3">
                                                        <i className="bi bi-clock-history text-success"></i> {moment(data.date).format('dddd, MMMM yyyy')}
                                                    </div>
                                                    <div className="col-md-3 col-sm-3 col-xs-3">
                                                        <i className="bi bi-link-45deg text-info"></i><a href={data.source} className="text-decoration-none link-dark">source</a>
                                                    </div>
                                                    <div className="col-md-3 col-sm-3 col-xs-3">
                                                        <i className="bi bi-share-fill text-primary p-5"></i>
                                                        <a href="#" className="text-decoration-none link-primary p-2"><i className="bi bi-facebook"></i></a>
                                                        <a href="#" className="text-decoration-none link-info p-2"><i className="bi bi-twitter"></i></a>
                                                    </div>
                                                    <div className="col-md-3 col-sm-3">
                                                        <Link to={"/NewsDetails/" + data.id} className="btn btn-sm btn-danger shadow rounded-pill" >Details...</Link>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        )
                    })
            }

        </div>
    )
}




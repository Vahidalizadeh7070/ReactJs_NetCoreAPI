import react, { useEffect, useState } from 'react';
import axios from 'axios';
import moment from 'moment';
import { BrowserRouter as Router, Switch, Route, Link, useParams } from "react-router-dom";
import { Facebook, Twitter } from 'react-sharingbuttons'
import 'react-sharingbuttons/dist/main.css'
import '../index.css';

export default function NewsDetails() {
    // the id of each post
    let { id } = useParams();
    const [Loading, setLoading] = useState(true);
    const [Title, setTitle] = useState('');
    const [About, setAbout] = useState('');
    const [DescriptionOne, setDescriptionOne] = useState('');
    const [DescriptionTwo, setDescriptionTwo] = useState('');
    const [DescriptionThree, setDescriptionThree] = useState('');
    const [ImageOne, setImageOne] = useState('');
    const [ImageTwo, setImageTwo] = useState('');
    const [ImageThree, setImageThree] = useState('');
    const [Date, setDate] = useState('');
    const [Category, setCategory] = useState('');
    const [Source, setSource] = useState('');
    const [VideoUrl, setVideoUrl] = useState('');
    // sharing links and texts
    const url = 'http://localhost:3000/NewsDetails/' + id
    const shareText = Title
    // the url of each image
    const BaseImageUrl = "https://localhost:44352/News/";

    useEffect(() => {
        if (id === undefined || id === "" || id === 0) {
            setLoading(true);
        }
        else {
            axios.get("https://localhost:44352/api/news/" + id).then((response) => {
                getData(response.data)
            }).catch((err) => err.name);
        }
    }, [])


    const getData = (data) => {
        setLoading(true)
        setTitle(data.title)
        setAbout(data.about)
        setDescriptionOne(data.descriptionOne)
        setDescriptionTwo(data.descriptionTwo)
        setDescriptionThree(data.descriptionThree)
        setImageOne(data.imageOne)
        setImageTwo(data.imageTwo)
        setImageThree(data.imageThree)
        setDate(data.date)
        setCategory(data.categories.categoryName)
        setSource(data.source)
        setVideoUrl(data.videoUrl)
        setTimeout(function () {
            setLoading(false)
        }, 1000);
    }
    return (

        <div className="container">
            <div className="shadow mt-5">
                <div className="p-5 m-5">
                    {Loading ? <div className="row align-content-center">
                        <div className="d-flex justify-content-center p-5">
                            <div className="spinner-border text-info spinner-size" role="status">
                                <span className="visually-hidden">Loading...</span>
                            </div>

                        </div>
                        <div className="d-flex justify-content-center p-5">
                            Please wait
                        </div>
                    </div> :
                        <div className="clearfix">
                            <img src={BaseImageUrl + ImageOne} className="col-md-6 img-fluid float-md-end mb-3 ms-md-3 shadow-lg rounded-3" alt="..." />
                            <h1 className="text-danger">
                                {Title}
                            </h1>
                            <hr />
                            <p className="lh-lg">
                                <h3 className="lh-lg">{About}</h3>
                            </p>
                            <div className="row pb-3">
                                <div className="col-md-10">
                                    <Facebook url={url} />
                                    <Twitter url={url} shareText={shareText} />
                                </div>
                            </div>
                            <h5 className="lh-lg">
                                {DescriptionOne}
                            </h5>
                            <div className="row mt-4">
                                <div className="col-md-5">
                                    <img src={BaseImageUrl + ImageTwo} className="img-fluid shadow-lg rounded-3" alt="..." />
                                </div>
                                <div className="col-md-7 ">
                                    <h4 className="text-secondary lh-lg">{DescriptionTwo}</h4>
                                </div>
                            </div>
                            <div className="row mt-5">
                                <div className="col-md-7 ">
                                    <h4 className="text-secondary lh-lg">{DescriptionThree}</h4>
                                </div>
                                <div className="col-md-5">
                                    <img src={BaseImageUrl + ImageThree} className="img-fluid shadow-lg rounded-3" alt="..." />
                                </div>
                            </div>
                            <hr/>
                            <div className="row pt-5 pb-5">
                                <div className="col-lg-4 col-md-4 col-sm-4">
                                    <i className="bi bi-calendar3 text-info"></i> {moment(Date).format('dddd, MMMM yyyy')}
                                </div>
                                <div className="col-lg-4 col-md-4 col-sm-4">
                                    <i className="bi bi-link-45deg text-danger"></i> <a href={Source} className="text-decoration-none link-dark">Source</a>
                                </div>
                                <div className="col-lg-4 col-md-4 col-sm-4">
                                <i className="bi bi-tags-fill text-success"></i> {Category}
                                </div>
                            </div>
                        </div>
                    }
                </div>
            </div>
        </div>
    )
}